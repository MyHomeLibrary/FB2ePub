using System.Xml;
using System.Xml.Linq;
using HTML5ClassLibrary.BaseElements.BlockElements;
using HTML5ClassLibrary.BaseElements.FormMenuOptions;
using HTML5ClassLibrary.BaseElements.InlineElements;
using HTML5ClassLibrary.BaseElements.InlineElements.TextBasedElements;
using HTML5ClassLibrary.BaseElements.Legends;
using HTML5ClassLibrary.BaseElements.ListElements;
using HTML5ClassLibrary.BaseElements.MapAreas;
using HTML5ClassLibrary.BaseElements.ObjectParameters;
using HTML5ClassLibrary.BaseElements.Ruby;
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
                        return new InlineFrame();
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
                    case Nav.ElementName:
                        return new Nav();
                    case NoScript.ElementName:
                        return new NoScript();
                    case ObjectElm.ElementName:
                        return new ObjectElm();
                    case OrderedList.ElementName:
                        return new OrderedList();
                    case OptionGroup.ElementName:
                        return new OptionGroup();
                    case Option.ElementName:
                        return new Option();
                    case Output.ElementName:
                        return new Output();
                    case Paragraph.ElementName:
                        return new Paragraph();
                    case Param.ElementName:
                        return new Param();
                    case PreFormated.ElementName:
                        return new PreFormated();
                    case Progress.ElementName:
                        return new Progress();
                    case ShortQuote.ElementName:
                        return new ShortQuote();
                    case RpElement.ElementName:
                        return new RpElement();
                    case RtElement.ElementName:
                        return new RtElement();
                    case RubyElement.ElementName:
                        return new RubyElement();
                    case SElement.ElementName:
                        return new SElement();
                    case Sample.ElementName:
                        return new Sample();
                    case Script.ElementName:
                        return new Script();
                    case Section.ElementName:
                        return new Section();
                    case Select.ElementName:
                        return new Select();
                    case SmallText.ElementName:
                        return new SmallText();
                    case Source.ElementName:
                        return new Source();
                    case Span.ElementName:
                        return new Span();
                    case Strong.ElementName:
                        return new Strong();
                    case Style.ElementName:
                        return new Style();
                    case Sub.ElementName:
                        return new Sub();
                    case Summary.ElementName:
                        return new Summary();
                    case Sup.ElementName:
                        return new Sup();
                    case Table.ElementName:
                        return new Table();
                    case TableBody.ElementName:
                        return new TableBody();
                    case TableData.ElementName:
                        return new TableData();
                    case TextArea.ElementName:
                        return new TextArea();
                    case TableFooter.ElementName:
                        return new TableFooter();
                    case HeaderCell.ElementName:
                        return new HeaderCell();
                    case TableHead.ElementName:
                        return new TableHead();
                    case Time.ElementName:
                        return new Time();
                    case Title.ElementName:
                        return new Title();
                    case TableRow.ElementName:
                        return new TableRow();
                    case Track.ElementName:
                        return new Track();
                    case UnorderedList.ElementName:
                        return new UnorderedList();
                    case Var.ElementName:
                        return new Var();
                    case Video.ElementName:
                        return new Video();
                    case WordBreakOportunity.ElementName:
                        return  new WordBreakOportunity();
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
