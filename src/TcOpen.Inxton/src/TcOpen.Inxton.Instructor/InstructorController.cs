using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TcOpen.Inxton.RepositoryDataSet;

namespace TcOpen.Inxton.Instructor
{
    public class InstructorController : INotifyPropertyChanged
    {
        public InstructorController(
            RepositoryDataSetHandler<InstructionItem> dataSet,
            IInstructionControlProvider instructionControlProvider = null
        )
        {
            this.InstructionDataSet = dataSet;
            InstructionControlProvider = instructionControlProvider;
            if (InstructionControlProvider != null)
                InstructionControlProvider.ChangeInstruction = RefreshInstruction;
        }

        InstructionItem currentInstruction;

        /// <summary>
        /// Gets current instruction item.
        /// </summary>
        public InstructionItem CurrentInstruction
        {
            // something to commit
            get { return currentInstruction; }
            set
            {
                if (currentInstruction == value)
                {
                    return;
                }

                currentInstruction = value;
                OnPropertyChanged(nameof(CurrentInstruction));
            }
        }

        /// <summary>
        /// Gets the instruction template provider.
        /// </summary>
        public IInstructionControlProvider InstructionControlProvider { get; }

        /// <summary>
        /// Gets current instruction set.
        /// </summary>
        public EntitySet<InstructionItem> CurrentInstructionSet { get; private set; } =
            new EntitySet<InstructionItem>();

        /// <summary>
        /// Gets instruction of this
        /// </summary>
        protected RepositoryDataSetHandler<InstructionItem> InstructionDataSet { get; }

        private InstructionItem EmptyIntruction = new InstructionItem();

        /// <summary>
        /// When overriden performs update of <see cref="CurrentInstruction"/>.
        /// </summary>
        public void RefreshInstruction(string key)
        {
            var instruction = this
                .CurrentInstructionSet.Items.Where(p => p.Key == key)
                .FirstOrDefault();

            if (instruction != null)
            {
                this.CurrentInstruction = instruction;
            }
            else
            {
                this.CurrentInstruction = EmptyIntruction;
            }
        }

        /// <summary>
        /// Loads instruction set from the repository to this controller.
        /// </summary>
        /// <param name="instructionSetId">Instrucion set id.</param>
        public void LoadInstructionSet(string instructionSetId)
        {
            var result = this.InstructionDataSet.Repository.Queryable.FirstOrDefault(p =>
                p._EntityId == instructionSetId
            );

            if (result == null)
            {
                this.InstructionDataSet.Create(instructionSetId, this.CurrentInstructionSet);
            }

            this.CurrentInstructionSet = this.InstructionDataSet.Read(instructionSetId);
        }

        /// <summary>
        /// Saves instruction set from this controller to the repository.
        /// </summary>
        /// <param name="instructionSetId">Instrucion set id.</param>
        public void SaveInstructionSet(string instructionSetId)
        {
            var result = this.InstructionDataSet.Repository.Queryable.FirstOrDefault(p =>
                p._EntityId == instructionSetId
            );

            if (result == null)
            {
                this.InstructionDataSet.Create(instructionSetId, this.CurrentInstructionSet);
            }
            this.InstructionDataSet.Update(instructionSetId, this.CurrentInstructionSet);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateFromTemplate()
        {
            if (this.InstructionControlProvider != null)
            {
                this.InstructionControlProvider.UpdateTemplate();
                this.UpdateStepList(this.InstructionControlProvider.InstructionSteps);
            }
        }

        private void UpdateStepList(IEnumerable<InstructionItem> templateInstructions)
        {
            if (templateInstructions == null)
                throw new ArgumentNullException(nameof(templateInstructions));

            var templateInstructionList = new List<InstructionItem>();
            var deletedInstructionList = new List<InstructionItem>(CurrentInstructionSet.Items);

            foreach (var item in templateInstructions)
            {
                item.Status = enumInstructionItemStatus.Active;
                templateInstructionList.Add(item);
            }

            foreach (var item in deletedInstructionList)
            {
                item.Status = enumInstructionItemStatus.Deleted;
            }

            var compared = deletedInstructionList
                .Union(templateInstructionList, new ListComparer())
                .ToList();

            CurrentInstructionSet.Items = compared;

            foreach (var item in CurrentInstructionSet.Items)
            {
                if (templateInstructions != null)
                {
                    var result = templateInstructions
                        .Where(p => p.Key == item.Key)
                        .FirstOrDefault();
                    if (result != null)
                    {
                        item.Status = enumInstructionItemStatus.Active;
                        item.Remarks = result.Remarks;
                    }
                }
            }
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(nameof(CurrentInstructionSet))
            );
        }
    }

    public class ListComparer : IEqualityComparer<InstructionItem>
    {
        public bool Equals(InstructionItem x, InstructionItem y)
        {
            return x.Key == y.Key;
        }

        public int GetHashCode(InstructionItem obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}
