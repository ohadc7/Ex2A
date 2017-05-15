using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

namespace GUI.ViewModel
{ 
    class MazeDefinitionsUserControlVM : ViewModel
    {
        private MazeDefinitionsUserControlModel model;

        public MazeDefinitionsUserControlVM()
        {
            this.model = new MazeDefinitionsUserControlModel();
        }
        
        public int MazeRowsDefinition
        {
            get { return model.MazeRowsDefinition; }
            set { NotifyPropertyChanged("MazeRowsDefinition"); }
        }

        public int MazeColsDefinition
        {
            get { return model.MazeColsDefinition; }
            set { NotifyPropertyChanged("MazeColsDefinition"); }
        }

        public string MazeNameDefinition
        {
            get { return model.MazeNameDefinition; }
            set { NotifyPropertyChanged("MazeNameDefinition"); }
        }
        
    }
}
