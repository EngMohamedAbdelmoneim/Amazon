namespace Amazon.Services.CategoryServices.Dto
{
    public class CategoryToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Type { get; set; }
        public string ParentCategoryName { get; set; }
    }
}
