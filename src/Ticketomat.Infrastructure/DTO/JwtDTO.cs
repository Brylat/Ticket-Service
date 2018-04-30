namespace Ticketomat.Infrastructure.DTO
{
    public class JwtDTO
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}