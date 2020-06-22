using System.Linq;
using model = Ordering.API.Models;

namespace Ordering.API.Services
{
    public class OrderAdapter
    {
        public static Order toGrpc(model.Order model)
        {
            var result = new Order
            {
                BuyerId = model.BuyerId,
                IdOrder = model.IdOrder
            };
            result.Lines.AddRange(model.Lines);
            result.States.AddRange(model.States.Select(s => new OrderState
            {
                State = s.Name,
                Data = s.Data
            }));
            return result;
        }

        public static model.Order fromGrpc(Order grpc) =>
            new model.Order
            {
                BuyerId = grpc.BuyerId,
                IdOrder = grpc.IdOrder,
                Lines = grpc.Lines,
                States = grpc.States.Select(s => new model.OrderState { 
                    Name = s.State,
                    Data = s.Data 
                }).ToList()
            };        
    }
}
