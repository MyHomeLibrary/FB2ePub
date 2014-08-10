using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.BaseElements.BlockElements;
using HTML5ClassLibrary.BaseElements.FormMenuOptions;
using HTML5ClassLibrary.BaseElements.InlineElements;
using HTML5ClassLibrary.BaseElements.Legends;
using HTML5ClassLibrary.BaseElements.ListElements;
using HTML5ClassLibrary.BaseElements.MapAreas;
using HTML5ClassLibrary.BaseElements.Structure_Header;
using HTML5ClassLibrary.BaseElements.TableElements;

namespace HTML5ClassLibrary.BaseElements
{
    public static class ElementFactory
    {
        public static IHTML5Item CreateElement(XNode xNode)
        {
            if (xNode.NodeType == XmlNodeType.Element)
            {
                var element = (XElement) xNode;
                switch (element.Name.LocalName)
                {
                    case Anchor.ElementName:
                        return new Anchor();
                    case Abbreviation.ElementName:
                        return new Abbreviation();
                    case Address.ElementName:
                        return new Address();
                    case Area.ElementName:
                        return new Area();
                    case Article.ElementName:
                        return new Article();
                    case Aside.ElementName:
                        return new Aside();
                    case Audio.ElementName:
                        return new Audio();
                    case BoldText.ElementName:
                        return new BoldText();
                    case Base.ElementName:
                        return new Base();
                    case BDI.ElementName:
                        return new BDI();
                    case BiDirectionalOverride.ElementName:
                        return new BiDirectionalOverride();
                    case BlockQuoteElement.ElementName:
                        return new BlockQuoteElement();
                    case Body.ElementName:
                        return new Body();
                    case EmptyLine.ElementName:
                        return new EmptyLine();
                    case Button.ElementName:
                        return new Button();
                    case Canvas.ElementName:
                        return new Canvas();
                    case TableCaption.ElementName:
                        return new TableCaption();
                    case Cite.ElementName:
                        return new Cite();
                    case CodeText.ElementName:
                        return new CodeText();
                    case ColElement.ElementName:
                        return new ColElement();
                    case ColGroup.ElementName:
                        return new ColGroup();
                    case Datalist.ElementName:
                        return new Datalist();
                    case DefinitionDescription.ElementName:
                        return new DefinitionDescription();
                    case DeletedText.ElementName:
                        return new DeletedText();
                    case Details.ElementName:
                        return new Details();
                    case Definition.ElementName:
                        return new Definition();
                    case Dialog.ElementName:
                        return new Dialog();
                    case Div.ElementName:
                        return new Div();
                    case DefinitionList.ElementName:
                        return new DefinitionList();
                    case DefinitionTerms.ElementName:
                        return new DefinitionTerms();
                    case EmphasisedText.ElementName:
                        return new EmphasisedText();
                    case Embed.ElementName:
                        return new Embed();
                    case FieldSet.ElementName:
                        return new FieldSet();
                    case FigCaption.ElementName:
                        return new FigCaption();
                    case Figure.ElementName:
                        return new Figure();
                    case Footer.ElementName:
                        return new Footer();
                    case Form.ElementName:
                        return new Form();
                    case H1.ElementName:
                        return new H1();
                    case H2.ElementName:
                        return new H2();
                    case H3.ElementName:
                        return new H3();
                    case H4.ElementName:
                        return new H4();
                    case H5.ElementName:
                        return new H5();
                    case H6.ElementName:
                        return new H6();
                    case Head.ElementName:
                        return new Head();
                    case Header.ElementName:
                        return new Header();
                    case HorizontalRule.ElementName:
                        return new HorizontalRule();
                    case HTML.ElementName:
                        return new HTML();
                    case ItalicText.ElementName:
                        return new ItalicText();
                    case InlineFrame.ElementName:
                        return new InlineFrame();;
                    case Image.ElementName:
                        return new Image();
                    case Input.ElementName:
                        return new Input();
                    case InsertedText.ElementName:
                        return new InsertedText();
                    case Kbd.ElementName:
                        return new Kbd();
                    case Keygen.ElementName:
                        return new Keygen();
                    case Label.ElementName:
                        return new Label();
                    case Legend.ElementName:
                        return new Legend();
                    case ListItem.ElementName:
                        return new ListItem();
                    case Link.ElementName:
                        return new Link();
                    case Main.ElementName:
                        return new Main();
                    case Map.ElementName:
                        return new Map();
                    case Mark.ElementName:
                        return new Mark();
                    case Menu.ElementName:
                        return new Menu();
                    case MenuItem.ElementName:
                        return new MenuItem();
                    case Meta.ElementName:
                        return new Meta();
                    case Meter.ElementName:
                        return new Meter();

                    case Source.ElementName:
                        return new Source();
                }
                
            }
            else if (xNode.NodeType == XmlNodeType.Text)
            {
                return new SimpleHTML5Text();
            }
            return null;
        }


    }
}
