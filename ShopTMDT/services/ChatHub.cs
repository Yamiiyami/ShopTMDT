using Microsoft.AspNetCore.SignalR;
using ShopTMDT.ViewModel;

namespace ShopTMDT.services
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IRatingRepository _repository;
        public ChatHub(IRatingRepository repository) 
        {
            _repository = repository;
        }


        public async Task SendMessage(string iduser, string message, int Star,int idprodcut)
        {
            var rating = new RatingVM()
            {
                DanhGia = message,
                IdHangHoa = idprodcut,
                IdUser = iduser,
                SoSao = Star,
            };
            _repository.Add(rating);


            await Clients.All.SendAsync("ReceiveMessage",iduser,message,Star, idprodcut, DateTime.Now );

        }

    }
}
