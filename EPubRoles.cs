using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPubLibrary
{
    public enum RolesEnum
    {
        Adapter,    // Use for a person who 1) reworks a musical composition, 
        //usually for a different medium, or 2) rewrites novels or 
        //stories for motion pictures or other audiovisual medium. 

        Annotator,  // Use for a person who writes manuscript annotations on a printed item

        Arranger,   // Use for a person who transcribes a musical composition, usually for a 
        //different medium from that of the original; in an arrangement the musical substance remains essentially unchanged. 

        Artist,     // Use for a person (e.g., a painter) who conceives, and perhaps also implements, 
        //an original graphic design or work of art, if specific codes (e.g., [egr], [etr]) are not desired. For book illustrators, prefer Illustrator [ill]. 

        AssociatedName, // Use as a general relator for a name associated with or found in an item or collection, or which 
        //cannot be determined to be that of a Former owner [fmo] or other designated relator indicative of provenance. 

        Author,         // Use for a person or corporate body chiefly responsible for the intellectual or artistic content of a work. 
        //This term may also be used when more than one person or body bears such responsibility. 

        AuthorInQuotationsOrTextExtracts,   // Use for a person whose work is largely quoted or extracted in a works to which 
        // he or she did not contribute directly. Such quotations are found particularly in exhibition catalogs, collections of photographs, etc. 

        AuthorOfAfterwordColophonEtc,       // Use for a person or corporate body responsible for an afterword, postface, colophon, etc. but who is not the chief author of a work. 

        AuthorOfIntroductionEtc,            // Use for a person or corporate body responsible for an introduction, preface, foreword, or other critical matter, but who is not the chief author. 

        BibliographicAntecedent,            // Use for the author responsible for a work upon which the work represented by the catalog record is based. This can be appropriate for adaptations, sequels, continuations, indexes, etc

        BookProducer,                       // Use for the person or firm responsible for the production of books and other print media, if specific codes (e.g., [bkd], [egr], [tyd], [prt]) are not desired. 

        Collaborator,                      // Use for a person or corporate body that takes a limited part in the elaboration of a work of another author or that brings complements (e.g., appendices, notes) to the work of another author. 

        Commentator,                        // Use for a person who provides interpretation, analysis, or a discussion of the subject 
        // matter on a recording, motion picture, or other audiovisual medium. 

        Compiler,                          // Use for a person who produces a work or publication by selecting and putting together material from the works of various persons or bodies. 

        Designer,                           // Use for a person or organization responsible for design if specific codes (e.g., [bkd], [tyd]) are not desired. 

        Editor,                            // Use for a person who prepares for publication a work not primarily his/her own, such as by elucidating text, adding introductory or other critical matter, or technically directing an editorial staff. 

        Illustrator,                       //  Use for the person who conceives, and perhaps also implements, a design or illustration, usually to accompany a written text. 

        Lyricist,                          // Use for the writer of the text of a song. 

        MetadataContact,                   //  Use for the person or organization primarily responsible for compiling and maintaining the original description of a metadata set (e.g., geospatial metadata set). 

        Musician,                          // Use for the person who performs music or contributes to the musical content of a work when it is not possible or desirable to identify the function more precisely. 

        Narrator,                          // Use for the speaker who relates the particulars of an act, occurrence, or course of events. 

        Other,                             // Use for relator codes from other lists which have no equivalent in the MARC list or for terms which have not been assigned a code. 

        Photographer,                      //  Use for the person or organization responsible for taking photographs, whether they are used in their original form or as reproductions. 

        Printer,                           // Use for the person or organization who prints texts, whether from type or plates. 

        Redactor,                          // Use for a person who writes or develops the framework for an item without being intellectually responsible for its content. 

        Reviewer,                          // Use for a person or corporate body responsible for the review of book, motion picture, performance, etc. 

        Sponsor,                           // Use for the person or agency that issued a contract, or under whose auspices a work has been written, printed, published, etc. 

        ThesisAdvisor,                     //  Use for the person under whose supervision a degree candidate develops and presents a thesis, memoir, or text of a dissertation. 

        Transcriber,                       //  Use for a person who prepares a handwritten or typewritten copy from original material, including from dictated or orally recorded material. 

        Translator,                        //  Use for a person who renders a text from one language into another, or from an older form of a language into the modern form. 

    }

    public static class EPubRoles
    {
        public static string ConvertEnumToAttribute(RolesEnum role)
        {
            switch (role)
            {
                case RolesEnum.Adapter:
                    return "adp";
                case RolesEnum.Annotator:
                    return "ann";
                case RolesEnum.Arranger:
                    return "arr";
                case RolesEnum.Artist:
                    return "art";
                case RolesEnum.AssociatedName:
                    return "asn";
                case RolesEnum.Author:
                    return "aut";
                case RolesEnum.AuthorInQuotationsOrTextExtracts:
                    return "aqt";
                case RolesEnum.AuthorOfAfterwordColophonEtc:
                    return "aft";
                case RolesEnum.AuthorOfIntroductionEtc:
                    return "aui";
                case RolesEnum.BibliographicAntecedent:
                    return "ant";
                case RolesEnum.BookProducer:
                    return "bkp";
                case RolesEnum.Collaborator:
                    return "clb";
                case RolesEnum.Commentator:
                    return "cmm";
                case RolesEnum.Compiler:
                    return "com";
                case RolesEnum.Designer:
                    return "dsr";
                case RolesEnum.Editor:
                    return "edt";
                case RolesEnum.Illustrator:
                    return "ill";
                case RolesEnum.Lyricist:
                    return "lyr";
                case RolesEnum.MetadataContact:
                    return "mdc";
                case RolesEnum.Musician:
                    return "mus";
                case RolesEnum.Narrator:
                    return "nrt";
                case RolesEnum.Other:
                    return "oth";
                case RolesEnum.Photographer:
                    return "pht";
                case RolesEnum.Printer:
                    return "prt";
                case RolesEnum.Redactor:
                    return "red";
                case RolesEnum.Reviewer:
                    return "rev";
                case RolesEnum.Sponsor:
                    return "spn";
                case RolesEnum.ThesisAdvisor:
                    return "ths";
                case RolesEnum.Transcriber:
                    return "trc";
                case RolesEnum.Translator:
                    return "trl";
            }

            return "oth";
        }
    }
}
