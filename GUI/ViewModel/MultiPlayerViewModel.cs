using GUI.Model;

namespace GUI.ViewModel
{
    class MultiPlayerViewModel : ViewModel
    {

        private IClientModel model;

        public MultiPlayerViewModel()
        {
            this.model = new ClientModel();
            this.model.Connect();
            //manage all the communication with the server
        }
    }
}
