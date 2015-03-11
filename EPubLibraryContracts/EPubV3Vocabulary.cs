using System;

namespace EPubLibraryContracts
{

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SerializationNameAttribute : Attribute
    {
        private readonly string _name;
        public SerializationNameAttribute(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; }}
    }

    public enum EpubV3Vocabulary
    {
        [SerializationName("cover")]
        Cover,          // The publications cover(s), jacket information, etc

        [SerializationName("frontmatter")]
        FrontMatter,    // Preliminary material to the main content of a publication, such as tables of contents, dedications, etc

        [SerializationName("bodymatter")]
        BodyMatter,     // The main content of a publication

        [SerializationName("backmatter")]
        BackMatter,     // Ancillary material occurring after the main content of a publication, such as indices, appendices, etc

        [SerializationName("volume")]
        Volume,         // A component of a collection

        [SerializationName("part")]
        Part,           // A major structural division of a piece of writing, typically encapsulating a set of related chapters

        [SerializationName("chapter")]
        Chapter,        // A major structural division of a piece of writing

        [SerializationName("subchapter")]
        SubChapter,     // A major sub-division of a chapter

        [SerializationName("division")]
        Division,       // A major structural division that may also appear as a substructure of a part (esp. in legislation)

        [SerializationName("foreword")]
        Foreword,       // An introductory section that precedes the work, typically not written by the work's author

        [SerializationName("preface")]
        Preface,        // An introductory section that precedes the work, typically written by the work's author

        [SerializationName("prologue")]
        Prologue,       // An introductory section that sets the background to a story, typically part of the narrative

        [SerializationName("introduction")]
        Introduction,   // A section in the beginning of the work, typically introducing the reader to the scope or nature of the work's content

        [SerializationName("preamble")]
        Preamble,       // A section in the beginning of the work, typically containing introductory and/or explanatory prose regarding the scope or nature of the work's content

        [SerializationName("conclusion")]
        Conclusion,     // An ending section that typically wraps up the work

        [SerializationName("epilogue")]
        Epilogue,       // A concluding section that is typically written from a later point in time than the main story, although still part of the narrative

        [SerializationName("afterword")]
        Afterword,      // A closing statement from the author or a person of importance to the story, typically providing insight into how the story came to be written, its significance or related events that have transpired since its timeline

        [SerializationName("epigraph")]
        Epigraph,       // A quotation that is pertinent but not integral to the text

        [SerializationName("toc")]
        TOC,            // A table of contents, typically appearing in the work's frontmatter, or at the beginning of a section

        [SerializationName("landmarks")]
        Landmarks,      // A collection of references to well-known/recurring components within the publication

        [SerializationName("loa")]
        LOA,            // A listing of audio clips included in the work

        [SerializationName("loi")]
        LOI,            // A listing of illustrations included in the work

        [SerializationName("lot")]
        LOT,            // A listing of tables included in the work

        [SerializationName("lov")]
        LOV,            // A listing of video clips included in the work

        [SerializationName("appendix")]
        Appendix,       // Supplemental information

        [SerializationName("colophon")]
        Colophon,       // A brief description usually located at the end of a publication, describing production notes relevant to the edition

        [SerializationName("index")]
        Index,          // A detailed list, usually arranged alphabetically, of the specific information in a publication

        [SerializationName("glossary")]
        Gloassary,      // An alphabetical list of terms in a particular domain of knowledge, with the definitions for those terms

        [SerializationName("glossterm")]
        GlossTerm,      // A glossary term (Required parent context: glossary)

        [SerializationName("glossdef")]
        GlossDef,       // The definition of a term in a glossary (Required parent context: glossary)

        [SerializationName("bibliography")]
        Bibliography,   // A list of works cited

        [SerializationName("biblioentry")]
        BiblioEntry,    // An entry in a bibliography

        [SerializationName("titlepage")]
        TitlePage,      // The title page of the work

        [SerializationName("halftitlepage")]
        HalfTitlePage,  // The half title page of the work (when present must preceed titlepage)

        [SerializationName("copyright-page")]
        CopyRightPage,  // The copyright page of the work

        [SerializationName("acknowledgments")]
        Acknowledgments,// A passage containing acknowledgments to entities involved in the realization of the work

        [SerializationName("imprint")]
        Imprint,        // Information relating to the publication or distribution of the work

        [SerializationName("imprimatur")]
        Imprimatur,     // A formal statement authorizing the publication of the work

        [SerializationName("contributors")]
        Contributors,   // A list of contributors to the work

        [SerializationName("other-credits")]
        OtherCredits,   // Acknowledgments of previously published parts of the work, illustration credits, and permission to quote from copyrighted material

        [SerializationName("errata")]
        Errata,         // Publication errata, in printed works typically a loose sheet inserted by hand; sometimes a bound page

        [SerializationName("dedication")]
        Dedication,     // An inscription addressed to one or several particular person(s)

        [SerializationName("revision-history")]
        RevisionHistory,// A record of changes made to a work

        [SerializationName("help")]
        Help,           // Information that clarifies or augments the content

        [SerializationName("notice")]
        Notice,         // Information that requires special attention, and that must not be skipped or suppressed. Examples include: alert, warning, caution, danger, important

        [SerializationName("halftitle")]
        HalfTitle,      // The title appearing on the first page of a work or immediately before the text

        [SerializationName("fulltitle")]
        FullTitle,      // The full title of the work, either simple, in which case it is identical to title, or compound, in which case it consists of a title and a subtitle

        [SerializationName("covertitle")]
        CoverTitle,     // The title of the work as displayed on the work's cover

        [SerializationName("title")]
        Title,          // The primary name of a work, section or component. When used in the context of a fulltitle, the primary part of the full title

        [SerializationName("subtitle")]
        SubTitle,       // An explanatory or alternate title for the work

        [SerializationName("bridgehead")]
        BridgeHead,     // A structurally insignificant heading that does not contribute to the hierarchical structure of the work

        [SerializationName("learning-objective")]
        LearningObjective,// An explicit designation or description of a learning objective or a reference to an explicit learning objective

        [SerializationName("learning-resource")]
        LearningResource,// A resource provided to enhance learning, or a reference to such a resource

        [SerializationName("assessment")]
        Assestment,     // A test, quiz, or other activity that helps measure a student's understanding of what is being taught

        [SerializationName("qna")]
        QNA,            // A question and answer section

        [SerializationName("footnote")]
        FootNote,       // A note appearing at the bottom of a page

        [SerializationName("rearnote")]
        RearNote,       // A note appearing in the rear (backmatter) of the work, or at the end of a section

        [SerializationName("footnotes")]
        FootNotes,      // A collection of notes appearing at the bottom of a page

        [SerializationName("rearnotes")]
        RearNotes,      // A collection of notes appearing at the rear (backmatter) of the work, or at the end of a section

        [SerializationName("noteref")]
        NoteRef,        // A reference to a note, typically appearing a superscripted symbol in the main body of text

        [SerializationName("keyword")]
        KeyWord,        // A key word or phrase

        [SerializationName("topic-sentence")]
        TopicSentance,  // A phrase or sentence serving as an introductory summary of the containing paragraph

        [SerializationName("concluding-sentence")]
        ConcludingSentance,// A phrase or sentence serving as a concluding summary of the containing paragraph

        [SerializationName("pagebreak")]
        PageBreak,      // A (sometimes valued) separator denoting the position before which a break between two contiguous pages occurs in a statically paginated media.

        [SerializationName("page-list")]
        PageList,       // A list of references to pagebreaks in a statically paginated media
    }
}
