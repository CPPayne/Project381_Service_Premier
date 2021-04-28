namespace BusinessLogic
{
    class Service
    {
        private string packageName;
        private string description;

        public Service(string packageName, string description)
        {
            this.PackageName = packageName;
            this.Description = description;
        }

        public string PackageName { get => packageName; set => packageName = value; }
        public string Description { get => description; set => description = value; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void createService()
        {

        }
    }
}
