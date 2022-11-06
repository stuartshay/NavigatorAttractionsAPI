namespace NavigatorAttractions.Service.Models.Attractions
{
    public class AttractionModel
    {
        public AttractionModel()
        {
            //this.Inventory = new InventoryModel();
            //this.Map = new MapModel();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Key { get; set; }

        public string FeatureKey { get; set; }

        public string Catalog { get; set; }

        public string ObjectType { get; set; }

        public string Display { get; set; }

        public string Slug { get; set; }

        public bool HasPhotos { get; set; }

        public DisplayDateModel DisplayDate { get; set; }

        public List<string> Keywords { get; set; }

        public List<string> Aliases { get; set; }

        //public InscriptionModel Inscription { get; set; }

        //public InventoryModel Inventory { get; set; }

        //public AttractionPhotoModel Photo { get; set; }

        //public LocModel loc { get; set; }

        //public MapModel Map { get; set; }

        public List<MachineTagModel> MachineTags { get; set; }

        //public List<ReferenceModel> References { get; set; }

        //public GeoCoordinateModel GeoCoordinate { get; set; }
    }
}