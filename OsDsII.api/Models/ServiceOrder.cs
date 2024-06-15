namespace OsDsII.Api.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public StatusServiceOrder Status { get; set; }
        public DateTimeOffset OpeningDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset FinishDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Comment> Comments { get; set; }

        public bool CanFinish()
        {
            return StatusServiceOrder.OPEN.Equals(Status);
        }
        public void FinishOS()
        {
            if (!CanFinish())
            {
                throw new Exception($"A ordem de serviço não pode ser concluída devido ao status atual {Status}");
            }

            Status = StatusServiceOrder.FINISHED;
            FinishDate = DateTimeOffset.Now;
        }

        public void Cancel()
        {
            if (!CanFinish())
            {
                throw new Exception($"A ordem de serviço não pode ser cancelada devido ao status atual {Status}");
            }

            Status = StatusServiceOrder.CANCELED;
        }
    }
}