namespace EPubLibraryContracts
{
    public enum EpubV3Vocabulary
    {
        Cover,          // The publications cover(s), jacket information, etc
        FrontMatter,    // Preliminary material to the main content of a publication, such as tables of contents, dedications, etc
        BodyMatter,     // The main content of a publication
        BackMatter,     // Ancillary material occurring after the main content of a publication, such as indices, appendices, etc
        Volume,         // A component of a collection
        Part,           // A major structural division of a piece of writing, typically encapsulating a set of related chapters
        Chapter,        // A major structural division of a piece of writing
        SubChapter,     // A major sub-division of a chapter
        Division,       // A major structural division that may also appear as a substructure of a part (esp. in legislation)
        Foreword,       // An introductory section that precedes the work, typically not written by the work's author
        Preface,        // An introductory section that precedes the work, typically written by the work's author
        Prologue,       // An introductory section that sets the background to a story, typically part of the narrative
        Introduction,   // A section in the beginning of the work, typically introducing the reader to the scope or nature of the work's content
        Preamble,       // A section in the beginning of the work, typically containing introductory and/or explanatory prose regarding the scope or nature of the work's content
        Conclusion,     // An ending section that typically wraps up the work
        Epilogue,       // A concluding section that is typically written from a later point in time than the main story, although still part of the narrative
        Afterword,      // A closing statement from the author or a person of importance to the story, typically providing insight into how the story came to be written, its significance or related events that have transpired since its timeline
        Epigraph,       // A quotation that is pertinent but not integral to the text
        TOC,            // A table of contents, typically appearing in the work's frontmatter, or at the beginning of a section
        Landmarks,      // A collection of references to well-known/recurring components within the publication
        LOA,            // A listing of audio clips included in the work
        LOI,            // A listing of illustrations included in the work
        LOV,            // A listing of video clips included in the work
        Appendix,       // Supplemental information
        Colophon,       // A brief description usually located at the end of a publication, describing production notes relevant to the edition
        Index,          // A detailed list, usually arranged alphabetically, of the specific information in a publication
        Gloassary,      // An alphabetical list of terms in a particular domain of knowledge, with the definitions for those terms
        GlossTerm,      // A glossary term (Required parent context: glossary)
        GlossDef,       // The definition of a term in a glossary (Required parent context: glossary)
        Bibliography,   // A list of works cited
        BiblioEntry,    // An entry in a bibliography
        TitlePage,      // The title page of the work
        HalfTitlePage,  // The half title page of the work (when present must preceed titlepage)
        CopyRightPage,  // The copyright page of the work
        Acknowledgments,// A passage containing acknowledgments to entities involved in the realization of the work
        Imprint,        // Information relating to the publication or distribution of the work
        Imprimatur,     // A formal statement authorizing the publication of the work
        Contributors,   // A list of contributors to the work
        OtherCredits,   // Acknowledgments of previously published parts of the work, illustration credits, and permission to quote from copyrighted material
        Errata,         // Publication errata, in printed works typically a loose sheet inserted by hand; sometimes a bound page
        Dedication,     // An inscription addressed to one or several particular person(s)
        RevisionHistory,// A record of changes made to a work
        Help,           // Information that clarifies or augments the content
        Notice,         // Information that requires special attention, and that must not be skipped or suppressed. Examples include: alert, warning, caution, danger, important
        HalfTitle,      // The title appearing on the first page of a work or immediately before the text
        FullTitle,      // The full title of the work, either simple, in which case it is identical to title, or compound, in which case it consists of a title and a subtitle
        CoverTitle,     // The title of the work as displayed on the work's cover
        Title,          // The primary name of a work, section or component. When used in the context of a fulltitle, the primary part of the full title
        SubTitle,       // An explanatory or alternate title for the work
        BridgeHead,     // A structurally insignificant heading that does not contribute to the hierarchical structure of the work
        LearningObjective,// An explicit designation or description of a learning objective or a reference to an explicit learning objective
        LearningResource,// A resource provided to enhance learning, or a reference to such a resource
        Assestment,     // A test, quiz, or other activity that helps measure a student's understanding of what is being taught
        QNA,            // A question and answer section
        FootNote,       // A note appearing at the bottom of a page
        RearNote,       // A note appearing in the rear (backmatter) of the work, or at the end of a section
        FootNotes,      // A collection of notes appearing at the bottom of a page
        RearNotes,      // A collection of notes appearing at the rear (backmatter) of the work, or at the end of a section
        NoteRef,        // A reference to a note, typically appearing a superscripted symbol in the main body of text
        KeyWord,        // A key word or phrase.
        TopicSentance,  // A phrase or sentence serving as an introductory summary of the containing paragraph
        ConcludingSentance,// A phrase or sentence serving as a concluding summary of the containing paragraph
        PageBreak,      // A (sometimes valued) separator denoting the position before which a break between two contiguous pages occurs in a statically paginated media.
        PageList,       // A list of references to pagebreaks in a statically paginated media
    }
}
