using EPubLibraryContracts;

namespace EPubLibrary
{

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
