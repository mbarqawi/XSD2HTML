using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;

namespace XSD2HTML
{
    public abstract class SchemaNode
    {
        protected int mMin;
        protected int mMax;
        protected string mXmlTag;
        protected string mNS;
        protected string mNSPrefix;
        protected int mDepth;
        protected string mNodeXPath;
        protected SchemaNode mParent;

        //Bug #89824 - To find the Attribute of a node.
        protected string mAttribute;

        protected const string NSPrefixBase = "nsIFG{0}";
        private static int NextPrefixIndex = 100;
        public string XSDType;

        #region Properties

        public int MinOccurs
        {
            get { return mMin; }
            set { mMin = value; }
        }

        public int MaxOccurs
        {
            get { return mMax; }
            set { mMax = value; }
        }
        [JsonProperty(Order = 1)]
        public string XmlTag
        {
            get => new string('@', this.Depth * 2).Replace("@", "&nbsp;") + " " + mXmlTag;
            set { mXmlTag = value; }
        }
        

        public string Attribute
        {
            get { return mAttribute; }
            set { mAttribute = value; }
        }

        public string Namespace
        {
            get { return mNS; }
            set { mNS = value; }
        }

        public int Depth
        {
            get { return mDepth; }
            set { mDepth = value; }
        }

        public string XPath
        {
            get { return mNodeXPath; }
            set { mNodeXPath = value; }
        }

        protected SchemaNode Parent
        {
            get { return mParent; }
            set { mParent = value; }
        }

        public Cardinality Cardinality
        {
            get
            {
                if (mMax == 1)
                {
                    if (mMin == 1)
                    {
                        return Cardinality.Required;
                    }

                    if (mMin == 0)
                    {
                        return Cardinality.Optional;
                    }
                }

                //mMax > 1 as < 0 is invalid
                return Cardinality.Repeating;
            }
        }

        public String Numbering { get; set; }
        public int ChildIndcater { get; internal set; }

        #endregion Properties

        #region Constructor

        public SchemaNode(int min, int max, string xmlTag, string ns, SchemaNode parent)
        {
            mMin = min;
            mMax = max;
            mXmlTag = xmlTag;
            mNS = ns;
            mParent = parent;
            mAttribute = Attribute;
        }

        #endregion Constructor

        protected string GenerateNSPrefix()
        {
            return string.Format(NSPrefixBase, NextPrefixIndex++);
        }

        public virtual bool Compile(Hashtable nameTable, StringCollection errors)
        {
            //compute nsPrefix
            if (!Helper.IsEmpty(mNS))
            {
                mNSPrefix = nameTable[mNS] as string;
                if (Helper.IsEmpty(mNSPrefix))
                {
                    mNSPrefix = GenerateNSPrefix();
                    nameTable.Add(mNS, mNSPrefix);
                }
            }

            //compute 'XPath'
            string basePath = mParent == null ? string.Empty : mParent.XPath;
            if (Helper.IsEmpty(mNSPrefix))
            {
                if (Helper.IsEmpty(mXmlTag))
                {
                    mNodeXPath = basePath;
                }
                else
                {
                    mNodeXPath = basePath + @"/" + mXmlTag;
                }
            }
            else
            {
                mNodeXPath = basePath + @"/" + mNSPrefix + ":" + mXmlTag;
            }

            return true;
        }

        /*
		 * SampleData is used by Infopath during design time to determine the complete
		 * structure of instances
		 *
		 * Step through all the nodes and generate empty values
		 */

        public abstract void GenerateSampleData(StringBuilder sb);

        /*
		 * Template is used by Infopath during run time to open a blank
		 * form
		 *
		 * Step through all the nodes and generate empty values for only the
		 * required ones. If minOccurs > 1, make sure you loop in that node
		 * and generate as many times
		 */

        public abstract void GenerateTemplate(StringBuilder sb);

        /*
		 * Fragment is required in the manifest for the following actions
		 *
		 * xReplace
		 * xOptional
		 * xCollection
		 *
		 */

        public abstract void GenerateFragment(StringBuilder sb);

        public virtual void Print(StringBuilder sb, bool isShort)
        {
        }
    }

    public enum Cardinality : int
    {
        Optional = 1,
        Required = 2,
        Repeating = 3
    }

    public class SimpleNode : SchemaNode
    {
        private int mMinLength;
        private int mMaxLength;

        public int MinLength
        {
            get { return mMinLength; }
            set { mMinLength = value; }
        }

        public int MaxLength
        {
            get { return mMaxLength; }
            set { mMaxLength = value; }
        }

        public string ElementType { get; set; }

        public SimpleNode(int min, int max, string xmlTag, string ns, SchemaNode parent)
            : base(min, max, xmlTag, ns, parent)
        {
            mMinLength = mMaxLength = -1;
        }

        public override void GenerateSampleData(StringBuilder sb)
        {
            //DCR #73672- FormatCode() is called to prepend the node tag with the ns0prefix in case of MX messages.
            //Helper.WriteStartElement(sb,Helper.FormatCode(mXmlTag));
            //         Helper.WriteEndElement(sb, Helper.FormatCode(mXmlTag));
        }

        public override void GenerateTemplate(StringBuilder sb)
        {
            for (int i = 0; i < mMin; i++)
            {
                //DCR #73672
                //Helper.WriteStartElement(sb, Helper.FormatCode(mXmlTag));
                //Helper.WriteEndElement(sb, Helper.FormatCode(mXmlTag));
            }
        }

        public override void GenerateFragment(StringBuilder sb)
        {
            if (mMin >= 1)
            {
                //DCR #73672
                //Helper.WriteStartElement(sb, Helper.FormatCode(mXmlTag));
                //Helper.WriteEndElement(sb, Helper.FormatCode(mXmlTag));
            }
        }

        public override void Print(StringBuilder sb, bool isShort)
        {
            //string fmt = "S({0}) {1} {2} {3} {4} {5} {6}\n";
            //if (!isShort)
            //	sb.Append(' ', Depth*2);
            //fmt = string.Format(fmt, Depth, mXmlTag, mNS, MinOccurs, MaxOccurs, MinLength,
            //	MaxLength);
            //sb.Append(fmt);
        }
    }

    public class EnumNode : SimpleNode
    {
        private StringCollection mEnumValues;

        public StringCollection EnumValues
        {
            get { return mEnumValues; }
        }

        public EnumNode(int min, int max, string xmlTag, string ns, SchemaNode parent,
            StringCollection enumValues)
            : base(min, max, xmlTag, ns, parent)
        {
            mEnumValues = enumValues;
        }

        public override void Print(StringBuilder sb, bool isShort)
        {
            string fmt = "E({0}) {1} {2} {3} {4} {5}\n";
            if (!isShort)
                sb.Append(' ', Depth * 2);
            fmt = string.Format(fmt, Depth, mXmlTag, mNS, MinOccurs, MaxOccurs, "#Values(" + mEnumValues.Count + ")");
            sb.Append(fmt);
        }
    }

    public class ComplexNode : SchemaNode
    {
        public ArrayList mChildList { get; set; }
        protected string mType;
        private int cildIndcater;

        public SchemaNode this[int index]
        {
            get { return (SchemaNode)mChildList[index]; }
        }

        public string Type
        {
            get { return mType; }
            set { mType = value; }
        }

        public int Count
        {
            get { return mChildList.Count; }
        }

        public void AddChild(SchemaNode node)
        {
            cildIndcater++;
            if (true)
            {
            }

            mChildList.Add(node);
        }

        public ComplexNode(int min, int max, string xmlTag, string nodeType, string ns, SchemaNode parent)
            : base(min, max, xmlTag, ns, parent)
        {
            mType = nodeType;
            cildIndcater = 0;
            mChildList = new ArrayList();
        }

        public ComplexNode(int min, int max, string xmlTag, string nodeType, string ns, SchemaNode parent, string attributeValue)
            : base(min, max, xmlTag, ns, parent)
        {
            mType = nodeType;
            mChildList = new ArrayList();
            mAttribute = attributeValue;
        }

        public virtual SchemaNode GetSibling(SchemaNode childNode, int relativeOffset)
        {
            int index = mChildList.IndexOf(childNode);
            int sibIndex = index + relativeOffset;

            if (index < 0 || sibIndex < 0 || sibIndex > mChildList.Count - 1)
            {
                return null;
            }

            return (SchemaNode)mChildList[sibIndex];
        }

        public override void GenerateSampleData(StringBuilder sb)
        {
            //DCR #73672
            //         Helper.WriteStartElement(sb, Helper.FormatCode(mXmlTag));
            ////Add atribute value.

            //foreach (SchemaNode node in mChildList)
            //{
            //	node.GenerateSampleData(sb);
            //}
            //         //DCR #73672
            //         Helper.WriteEndElement(sb, Helper.FormatCode(mXmlTag));
        }

        public override void GenerateTemplate(StringBuilder sb)
        {
            //for(int i=0; i<mMin; i++)
            //{
            //             //DCR #73672
            //             Helper.WriteStartElement(sb, Helper.FormatCode(mXmlTag));

            //	foreach (SchemaNode node in mChildList)
            //	{
            //		node.GenerateTemplate(sb);
            //	}
            //             //DCR #73672
            //             Helper.WriteEndElement(sb, Helper.FormatCode(mXmlTag));
            //}
        }

        public override void GenerateFragment(StringBuilder sb)
        {
            //if (mMin >= 1)
            //{
            //             //DCR #73672 - To prepend the ns0 prefix with every MX node.

            //             // Bug #89824 -Attribute bug to insert the Attributes in the fragments of Complex nodes in manifest.xsf. for e.g Ccy=""
            //             if (mAttribute != null)
            //                 Helper.WriteStartElement(sb, Helper.FormatCode(mXmlTag) + " " + mAttribute + @"=""""");
            //             else
            //             Helper.WriteStartElement(sb, Helper.FormatCode(mXmlTag));

            //	foreach (SchemaNode node in mChildList)
            //	{
            //		node.GenerateFragment(sb);
            //	}
            //             //DCR #73672
            //             Helper.WriteEndElement(sb, Helper.FormatCode(mXmlTag));
            //}
        }

        public override bool Compile(Hashtable nameTable, StringCollection errors)
        {
            base.Compile(nameTable, errors);
            foreach (SchemaNode node in mChildList)
            {
                node.Compile(nameTable, errors);
            }
            return true;
        }

        public override void Print(StringBuilder sb, bool isShort)
        {
            string fmt = "C({0}) {1} {2} {3} {4}\n";
            if (!isShort)
                sb.Append(' ', Depth * 2);
            fmt = string.Format(fmt, Depth, mXmlTag, mNS, MinOccurs, MaxOccurs/*, XPath*/);
            sb.Append(fmt);

            if (!isShort)
            {
                foreach (SchemaNode nd in mChildList)
                    nd.Print(sb, false);
            }
        }
    }

    internal class ChoiceNode : ComplexNode
    {
        public ChoiceNode(int min, int max, string xmlTag, string nodeType, string ns, SchemaNode parent)
            : base(min, max, xmlTag, nodeType, ns, parent)
        {
        }

        public override void GenerateSampleData(StringBuilder sb)
        {
            foreach (SchemaNode node in mChildList)
            {
                node.GenerateSampleData(sb);
            }
        }

        public override void GenerateTemplate(StringBuilder sb)
        {
            for (int i = 0; i < mMin; i++)
            {
                ((SchemaNode)mChildList[0]).GenerateTemplate(sb);
            }
        }

        public override void GenerateFragment(StringBuilder sb)
        {
            if (mMin >= 1)
            {
                ((SchemaNode)mChildList[0]).GenerateFragment(sb);
            }
        }

        public override void Print(StringBuilder sb, bool isShort)
        {
            string fmt = "CHOICE({0}) {1} {2} {3} {4}\n";
            if (!isShort)
                sb.Append(' ', Depth * 2);
            fmt = string.Format(fmt, Depth, mXmlTag, mNS, MinOccurs, MaxOccurs);
            sb.Append(fmt);

            if (!isShort)
            {
                foreach (SchemaNode nd in mChildList)
                    nd.Print(sb, false);
            }
        }
    }

    internal class SequenceNode : ComplexNode
    {
        public SequenceNode(int min, int max, string xmlTag, string nodeType, string ns, SchemaNode parent)
            : base(min, max, xmlTag, nodeType, ns, parent)
        {
        }

        public override void GenerateSampleData(StringBuilder sb)
        {
            foreach (SchemaNode node in mChildList)
            {
                node.GenerateSampleData(sb);
            }
        }

        public override void GenerateTemplate(StringBuilder sb)
        {
            for (int i = 0; i < mMin; i++)
            {
                foreach (SchemaNode node in mChildList)
                {
                    node.GenerateTemplate(sb);
                }
            }
        }

        public override void GenerateFragment(StringBuilder sb)
        {
            if (mMin >= 1)
            {
                foreach (SchemaNode node in mChildList)
                {
                    node.GenerateFragment(sb);
                }
            }
        }

        public override void Print(StringBuilder sb, bool isShort)
        {
            string fmt = "SEQUENCE({0}) {1} {2} {3} {4}\n";
            if (!isShort)
                sb.Append(' ', Depth * 2);
            fmt = string.Format(fmt, Depth, mXmlTag, mNS, MinOccurs, MaxOccurs);
            sb.Append(fmt);

            if (!isShort)
            {
                foreach (SchemaNode nd in mChildList)
                    nd.Print(sb, false);
            }
        }
    }
}