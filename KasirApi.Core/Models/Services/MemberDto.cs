namespace KasirApi.Repository.Entities

    public partial class MemberDto
    {
        public int Id { get; set; }
        public string MemberNumber { get; set; } = null!;
        public string Nik { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string BirthPlace { get; set; } = null!;
        public char? Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Address { get; set; }
        public int DataStatusId { get; set; }
        public int Point { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

public class MemberViewDto : MemberDto{}

public class MemberAddDto : MemberDto{}

public class MemberUpdDto : MemberDto{}

public class MemberFilter : MemberDto{}