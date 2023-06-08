using TcOpen.Inxton.RepositoryDataSet;

namespace TcoDrivesBeckhoff
{
    public class PositioningParamItem :  IDataSetItems
    {
        private string key;

        /// <summary>
        /// Gets or sets the key of this instruction item (list of process set).
        /// </summary>
        public string Key
        {
            get => key;
            set
            {
                if (key == value)
                {
                    return;
                }

                key = value;
            
            }
        }


        private string description;
        private PlainTcoSingleAxisMoveParam moveParam;


        /// <summary>
        /// gets or sets additional information. 
        /// </summary>
        public string Description
        {
            get => description;
            set
            {
                if (description == value)
                {
                    return;
                }

                description = value;
            }
        }

        public PlainTcoSingleAxisMoveParam MoveParam
        {
            get => moveParam; set
            {
                moveParam = value;
            }
        }


    }

}
