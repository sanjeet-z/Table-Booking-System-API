namespace ApplicationLayer.DTOs.AuthDto.RequestDtos
{
    public class QueryRequestDto
    {
        public string? filterOn { get; set; }
        public string? filterQuery { get; set; }
        public string? sortBy { get; set; }
        public bool? isAscending { get; set; }
        public int pageNo { get; set; } = 1;
        public int pageSize { get; set; } = 1000;
    }
}
