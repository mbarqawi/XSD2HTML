using Newtonsoft.Json;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Schema;
using XSD2HTML;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private static int mCount;
        private XmlSchema mSchema;

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadXsd(String xsdPath)
        {
            try
            {
                var coll = new XmlSchemaCollection();
              
              
                coll.Add(null, xsdPath);
                var ElementList = new List<XmlSchemaElement>();

                foreach (var schemaItem in coll)
                    foreach (XmlSchemaElement element in schemaItem.Elements.Values)
                        ElementList.Add(element);
                listBoxXsdEle.DataSource = ElementList;
                listBoxXsdEle.DisplayMember = "Name";
                this.Text = xsdPath;
                tabControl1.SelectedIndex =0 ;
            }
            catch (XmlSchemaException e)
            {
                WriteLine(string.Format("LineNumber = {0}", e.LineNumber));
                WriteLine(string.Format("LinePosition = {0}", e.LinePosition));
                WriteLine(string.Format("Message = {0}", e.Message));
                WriteLine(string.Format("File = {0}", e.SourceUri));
                tabControl1.SelectedIndex = 1;

            }
            catch (Exception e)
            {
                WriteLine(e.ToString());
                tabControl1.SelectedIndex = 1;
            }
        }

        private void WriteLine(string Message)
        {
            textBoxOutput.AppendText(Message);
            textBoxOutput.AppendText(Environment.NewLine);
        }

        /*
* Used to parse a complex type
*/

        private SchemaNode ParseComplexType(SchemaNode parent,
            XmlSchemaComplexType ct)
        {
            SchemaNode node = null;

            if (ct.ContentModel == null && ct.Particle != null)
            {
                if ((ct.Particle is XmlSchemaChoice))
                {
                    parent.XSDType = "Choice";
                    var schemaBase = (XmlSchemaGroupBase)ct.Particle;
                    if (schemaBase.Items != null)
                        for (var i = 0; i < schemaBase.Items.Count; i++)
                        {
                            var particle = (XmlSchemaParticle)schemaBase.Items[i];
                            node = ParseParticle(parent, particle);
                        }

                }
                else if(ct.Particle is XmlSchemaSequence)
                {
                    /*
                     * Parse each particle inside the ComplexType
                     */
                    var schemaBase = (XmlSchemaGroupBase)ct.Particle;
                    if (schemaBase.Items != null)
                        for (var i = 0; i < schemaBase.Items.Count; i++)
                        {
                            var particle = (XmlSchemaParticle)schemaBase.Items[i];
                            node = ParseParticle(parent, particle);
                        }
                }
            }

            return node;
        }

        private SchemaNode ParseParticle(SchemaNode parent, XmlSchemaParticle pa)
        {
            SchemaNode node = null;
            //DCR #100598 - Increment control count for every element.
            mCount++;
            if (pa is XmlSchemaElement)
            {
                node = ParseElement(parent, (XmlSchemaElement)pa);
            }
            else if (pa is XmlSchemaAny)
            {
                //todo
                //to be only allowed for signature element wit ns "http://www.w3.org/2000/09/xmldsig#"
            }
            else if (pa is XmlSchemaGroupBase)
            {
                if (pa is XmlSchemaAll)
                {
                    Debug.Assert(false, "All group found");
                }
                else if (pa is XmlSchemaSequence)
                {
                    var sequence = (XmlSchemaSequence)pa;
                    var maxOccurs =
                        string.Compare(sequence.MaxOccursString, "unbounded", StringComparison.Ordinal) ==
                        0 //fx-cop correction
                            ? -1
                            : (int)sequence.MaxOccurs;
                    var minOccurs = (int)sequence.MinOccurs;

                    var sequenceNode = new SequenceNode(minOccurs, maxOccurs, null,
                        null, null, parent);

                    SetDepth(sequenceNode, parent);
                    ((ComplexNode)parent).AddChild(sequenceNode);

                    parent = sequenceNode;

                    if (sequence.Items != null)
                        for (var i = 0; i < sequence.Items.Count; i++)
                            node = ParseParticle(parent, (XmlSchemaParticle)sequence.Items[i]);
                }
                else if (pa is XmlSchemaChoice)
                {
                    var choice = (XmlSchemaChoice)pa;
                    var maxOccurs =
                        string.Compare(choice.MaxOccursString, "unbounded", StringComparison.Ordinal) ==
                        0 //fx-cop correction
                            ? -1
                            : (int)choice.MaxOccurs;
                    var minOccurs = (int)choice.MinOccurs;

                    var choiceNode = new ChoiceNode(minOccurs, maxOccurs, null,
                        null, null, parent);

                    SetDepth(choiceNode, parent);
                    ((ComplexNode)parent).AddChild(choiceNode);

                    parent = choiceNode;

                    if (choice.Items != null)
                        for (var i = 0; i < choice.Items.Count; i++)
                            node = ParseParticle(parent, (XmlSchemaParticle)choice.Items[i]);
                }
            }
            else if (pa is XmlSchemaGroupRef)
            {
                ParseParticle(parent, ((XmlSchemaGroupRef)pa).Particle);
            }

            return node;
        }

        private void SetDepth(SchemaNode node, SchemaNode parent)
        {
            if (parent == null)
                node.Depth = 0;
            else
                node.Depth = parent.Depth + 1;
        }

        private SchemaNode ParseElement(SchemaNode parent,
            XmlSchemaElement elem)
        {
            SchemaNode node = null;
            var refElem = elem;

            if (elem.Name == null)
            {
                refElem = (XmlSchemaElement)mSchema.Elements[elem.RefName];

            }

            var maxOccurs =
                string.Compare(elem.MaxOccursString, "unbounded", StringComparison.Ordinal) == 0 //fx-cop correction
                    ? -1
                    : (int)elem.MaxOccurs;
            var minOccurs = (int)elem.MinOccurs;

            /* Bug #89824 - MX Messages contain complex nodes having attribute nodes as child nodes.
             * For that, find attribute of each complex node, if it is not null, create a separate control
             * for the attribute node.*/

            string attribute = null;
            if (elem.ElementType is XmlSchemaComplexType)
            {
                var ct = (XmlSchemaComplexType)refElem.ElementType;
                if (ct != null)
                    if (ct.Attributes != null)
                        foreach (DictionaryEntry obj in ct.AttributeUses)
                            attribute = (obj.Value as XmlSchemaAttribute).Name;
                ComplexNode complexNode = null;
                //Bug #89824
                if (attribute != null) // Complex node having attribute as a parameter.
                    complexNode = new ComplexNode(minOccurs, maxOccurs, elem.QualifiedName.Name,
                        elem.SchemaTypeName.ToString(), elem.QualifiedName.Namespace, parent, attribute);
                else
                    complexNode = new ComplexNode(minOccurs, maxOccurs, elem.QualifiedName.Name,
                        elem.SchemaTypeName.ToString(), elem.QualifiedName.Namespace, parent);
                SetDepth(complexNode, parent);

                node = complexNode;
                ParseComplexType(node, ct);
            }
            else if (elem.ElementType is XmlSchemaSimpleType)
            {
                var sType = (XmlSchemaSimpleType)elem.ElementType;

                int minL, maxL;
                minL = maxL = -1;
                ArrayList enumList = null;
            
                if (sType.Content is XmlSchemaSimpleTypeRestriction)
                {
                    var sr = (XmlSchemaSimpleTypeRestriction)sType.Content;
                    var objC = sr.Facets;

                    foreach (object obj in objC)
                        if (obj is XmlSchemaMinLengthFacet)
                        {
                            minL = int.Parse(((XmlSchemaMinLengthFacet)obj).Value);
                        }
                        else if (obj is XmlSchemaMaxLengthFacet)
                        {
                            maxL = int.Parse(((XmlSchemaMaxLengthFacet)obj).Value);
                        }
                        else if (obj is XmlSchemaLengthFacet)
                        {
                            minL = int.Parse(((XmlSchemaLengthFacet)obj).Value);
                            maxL = minL;
                        }
                        else if (obj is XmlSchemaEnumerationFacet)
                        {
                            if (Helper.IsEmpty(enumList)) enumList = new ArrayList(objC.Count);

                            enumList.Add(((XmlSchemaEnumerationFacet)obj).Value);
                        }
                }

                //now determine what type of node to generate
                if (!Helper.IsEmpty(enumList))
                {
                    enumList.Sort();
                    var coll = new StringCollection();
                    foreach (string str in enumList) coll.Add(str);

                    var enumNode = new EnumNode(minOccurs, maxOccurs, elem.QualifiedName.Name,
                        elem.QualifiedName.Namespace, parent, coll);
                    node = enumNode;
                }
                else
                {
                    var simpleNode = new SimpleNode(minOccurs, maxOccurs, elem.QualifiedName.Name,
                        elem.QualifiedName.Namespace, parent);

                    if (minL >= 0) simpleNode.MinLength = minL;

                    if (maxL >= 0) simpleNode.MaxLength = maxL;
                    simpleNode.ElementType = Enum.GetName(typeof(System.Xml.Schema.XmlTypeCode), elem.ElementSchemaType.TypeCode);
                    node = simpleNode;
                }
               
                SetDepth(node, parent);
            }
            else if (elem.ElementType is XmlSchemaDatatype)
            {
                var simpleNode = new SimpleNode(minOccurs, maxOccurs, elem.QualifiedName.Name,
                    elem.QualifiedName.Namespace, parent);
                simpleNode.ElementType = Enum.GetName(typeof(System.Xml.Schema.XmlTypeCode), elem.ElementSchemaType.TypeCode);

                if (simpleNode.ElementType == "")
                {
                    int o = 0;
                }
                node = simpleNode;
                SetDepth(node, parent);
            }

            if (parent != null) ((ComplexNode)parent).AddChild(node);

            return node;
        }

        private void BuildSchema(Hashtable nsPrefixTable, XmlSchemaElement elem)
        {
            try
            {
                SchemaNode mRootNode;

                if (elem == null)
                {
                    IEnumerator e = mSchema.Items.GetEnumerator();
                    while (e.MoveNext())
                        if (e.Current is XmlSchemaElement)
                        {
                            elem = (XmlSchemaElement)e.Current;
                            break;
                        }
                }

                if (elem != null)
                {
                    mSchema =(XmlSchema) elem.Parent;
                    mRootNode = (ComplexNode)ParseElement(null, elem);

                    var jsonString = JsonConvert.SerializeObject(mRootNode);
                    var errors = new StringCollection();
                    textBoxOutput.Text = jsonString;

                    SetNumbering(mRootNode);

                    RenderHtml(mRootNode);
                }
            }

#pragma warning suppress 6500
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                textBoxOutput.Text = e.Message;
            }
        }

        //Set Numbering
        private void SetNumbering(SchemaNode mRootNode)
        {
            for (var i = 0; i < ((ComplexNode)mRootNode).mChildList.Count; i++)
            {
                var targetNode = ((ComplexNode)mRootNode)[i];
                targetNode.Numbering = (mRootNode.Numbering + "." + (i + 1)).TrimStart('.');
                if (targetNode is ComplexNode) SetNumbering(((ComplexNode)mRootNode)[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void RenderHtml(SchemaNode XsdModel)
        {
            // Tag;

            webBrowserHtml.Navigate("about:blank");
            webBrowserHtml.Document.OpenNew(false);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Template.cshtml");
            var template = File.ReadAllText(path);

            var result = Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), typeof(SchemaNode), XsdModel);

            webBrowserHtml.Document.Write(result);
            webBrowserHtml.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            BuildSchema(null, (XmlSchemaElement)listBoxXsdEle.SelectedItem);
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var dropped = (string[])e.Data.GetData(DataFormats.FileDrop);
            var fileName = dropped[0];
            if (System.IO.Path.GetExtension(fileName).Contains("xsd"))
            {
                LoadXsd(fileName);
            }
            else
            {
                MessageBox.Show("This is Not Xsd");
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void buttonGenrateHtml_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButtonGenrateHtml_Click(object sender, EventArgs e)
        {
            BuildSchema(null, (XmlSchemaElement)listBoxXsdEle.SelectedItem);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XSD|*.XSD";
            openFileDialog1.Title = "Select a schema file";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                LoadXsd(fileName);
            }
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            if (webBrowserHtml.Document != null)
                if (webBrowserHtml.Document.Body != null)
                    Clipboard.SetText(webBrowserHtml.Document.Body.InnerHtml, TextDataFormat.Html);
        }

        private void listBoxXsdEle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BuildSchema(null, (XmlSchemaElement)listBoxXsdEle.SelectedItem);
        }
    }
}