using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Vortex.Connector;

namespace TcoCore
{
    /// <summary>
    /// Provides access to messages of the object and all child objects.
    /// </summary>
    public class TcoObjectMessageHandler : INotifyPropertyChanged
    {
        private List<IValueTag> _categoryTags;
        private readonly IsTcoContext _context;
        private List<IValueTag> _cycleTags;
        private IEnumerable<TcoMessage> _descendingMessages;
        private readonly IsTcoObject _obj;

        /// <summary>
        /// Create new instance of <see cref="TcoObjectMessageHandler"/>.
        /// </summary>        
        [Obsolete("Do not use! Required for WPF")]
        public TcoObjectMessageHandler()
        {
            _context = new TcoContext();
            _obj = new TcoObject();
        }

        /// <summary>
        /// Creates new instance of <see cref="TcoObjectMessageHandler"/>
        /// </summary>
        /// <param name="context">TcoObject's context</param>
        /// <param name="obj">TcoObject</param>
        public TcoObjectMessageHandler(IsTcoContext context, IsTcoObject obj)
        {
            _context = context;
            _obj = obj;
        }

        /// <summary>
        /// Property change notification implementation.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private IEnumerable<TcoMessage> GetObjectMessages()
        {
            return _obj.GetDescendants<TcoMessage>(this.DiagnosticsDepth);
        }

        private void ReadCategories()
        {
            _obj.GetConnector().ReadBatchFromUIThread(CategoryTags);
        }

        private void ReadCycles()
        {
            _obj.GetConnector().ReadBatchFromUIThread(CycleTags);
        }

        private List<IValueTag> CategoryTags
        {
            get
            {
                if (_categoryTags == null)
                {
                    _categoryTags = new List<IValueTag>();
                    _categoryTags.AddRange(DescendingMessages.Select(p => p.Category));
                }

                return _categoryTags;
            }
        }

        private List<IValueTag> CycleTags
        {
            get
            {
                if (_cycleTags == null)
                {
                    _cycleTags = new List<IValueTag>();
                    if (_context != null) { _cycleTags.Add(_context.StartCycleCount); }
                    _cycleTags.AddRange(DescendingMessages.Select(p => p.Cycle));
                    _cycleTags.AddRange(DescendingMessages.Select(p => p.Pinned));
                }

                return _cycleTags;
            }
        }

        private IEnumerable<TcoMessage> DescendingMessages
        {
            get
            {
                if (_descendingMessages == null)
                {
                    _descendingMessages = GetObjectMessages();
                }

                return _descendingMessages;
            }
        }

        private IEnumerable<TcoMessage> ActiveMessages
        {
            get
            {
                return DescendingMessages.Where(p => p.IsActive);
            }
        }
      
        string highestSeverityMessage;
        private int diagnosticsDepth = 15;
        

        public string HighestSeverityMessage
        {
            get => highestSeverityMessage; set
            {
                if (highestSeverityMessage == value)
                {
                    return;
                }

                highestSeverityMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HighestSeverityMessage)));
            }
        }

        private List<IValueTag> refreshTags { get; set; }

        /// <summary>
        /// Performs refresh of the messages of this <see cref="TcoObject"/> and all its child object.
        /// </summary>
        /// <returns>Enumerable of messages as POCO object.</returns>
        public IEnumerable<PlainTcoMessage> GetActiveMessages(eMessageCategory  category = eMessageCategory.All)
        {
            if (refreshTags == null)
            {
                refreshTags = new List<IValueTag>();
                refreshTags?.AddRange(CycleTags);
                refreshTags?.AddRange(CategoryTags);
            }

            // We must check that the connector did start R/W operations loop, due to possible dead lock at start-up
            // Reported to Inxton team as FOXTROTH #564

            if (_obj.GetConnector().RwCycleCount > 1)
            {
                _obj.GetConnector().ReadBatchFromUIThread(refreshTags);
            }

            return DescendingMessages?.Where(p => p.IsActive && p.Category.LastValue >= (short)category).Select(p => p.PlainMessage);
        }

        /// <summary>
        /// Executes quick update of the messages of this <see cref="TcoObject"/> and all its child object.
        /// Updates <see cref="ActiveMessagesCount"/> and <see cref="HighestSeverity"/> properties.
        /// </summary>
        public void UpdateHealthInfo()
        {
            ReadCycles();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveMessagesCount)));
            if (ActiveMessagesCount > 0)
            {
                ReadCategories();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HighestSeverity)));
                HighestSeverityMessage = ActiveMessages.OrderByDescending(p => p.Category.LastValue).FirstOrDefault()?.PlainMessage.Text;
            }
            else
            {
                HighestSeverityMessage = string.Empty;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HighestSeverity)));
            }
        }

        /// <summary>
        /// Gets the number of active messages of this object and its descendants
        /// The property is updated on <see cref="UpdateHealthInfo"/> method call.
        /// </summary>
        public int ActiveMessagesCount
        {
            get
            {
                return ActiveMessages.Count();
            }
        }

        /// <summary>
        /// Gets the highest category from messages of this object and its descendants.
        /// The property is updated on <see cref="UpdateHealthInfo"/> method call.
        /// </summary>
        public eMessageCategory HighestSeverity
        {
            get
            {
                if (ActiveMessagesCount > 0)
                {
                    return (eMessageCategory)ActiveMessages.Max(p => p.Category.LastValue);

                }

                return eMessageCategory.None;
            }
        }

        /// <summary>
        /// Gets or sets the diagnostics depth for this object. The object's tree will be searched for messaging objects up to diagnostics depth set in this property.
        /// </summary>
        public int DiagnosticsDepth { get => diagnosticsDepth; set { diagnosticsDepth = value;  this._descendingMessages = null;  _categoryTags = null; _cycleTags = null; refreshTags = null; } }
    }
}
