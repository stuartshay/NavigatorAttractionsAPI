using System.Collections;

namespace NavigatorAttractions.Service.Results
{
    public class PhotoStatusResult
    {
        public string PhotoId { get; set; }

        public string Status { get; set; }
    }

    public class PhotoCollectionStatusResult : IEnumerable<PhotoStatusResult>
    {
        public List<PhotoStatusResult> PhotoStatusResults { get; set; }

        public PhotoStatusResult this[int index]
        {
            get => PhotoStatusResults[index];
            set => PhotoStatusResults.Insert(index, value);
        }

        public IEnumerator<PhotoStatusResult> GetEnumerator()
        {
            return PhotoStatusResults.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
