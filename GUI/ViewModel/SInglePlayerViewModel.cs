
using GUI.Model;

namespace GUI.ViewModel
{
    class SinglePlayerViewModel : ViewModel
    {
        private IClientModel model;

        public SinglePlayerViewModel()
        {
            this.model = new ClientModel();
            //manage all the communication with the server

        }
    }
}
