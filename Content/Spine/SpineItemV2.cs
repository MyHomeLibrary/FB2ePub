
namespace EPubLibrary.Content.Spine
{
    public class SpineItemV2
    {

        public string Name { get; set; }

        // the following is for V3 only

        private bool _linear = true;

        public bool Linear
        {
            get { return _linear; } 
            set { _linear = value; }
        }


        public string ID { get; set; }

    }
}