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
            return result;
        }

        public static model.Order fromGrpc(Order grpc) =>
            new model.Order
            {
                BuyerId = grpc.BuyerId,
                IdOrder = grpc.IdOrder,
                Lines = grpc.Lines
            };        
    }
}
