namespace API_EXAMEN_APP.DTO
{
    public class SubscribeDTO
    {
        public int UserId { get; set; }
        public DateOnly InscriptionAt { get; set; }
        public DateOnly ExpirationAt { get; set; }
        public int SubTypeId { get; set; }

    }
}
