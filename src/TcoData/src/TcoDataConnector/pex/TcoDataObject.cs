using System;
using System.Linq;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Data;
using Vortex.Connector;

namespace TcoData
{

    public partial class TcoDataExchange
    {
        private dynamic _onliner;

        protected dynamic Onliner
        {
            get
            {
                if (this._onliner == null)
                {
                    var dataProperty = this.GetType().GetProperties().ToList().Find(p => p.Name == "_data");

                    if (dataProperty == null)
                    {
                        dataProperty = this.GetType().GetProperty("_data", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    }

                    if (dataProperty != null)
                    {
                        var exchangableObject = dataProperty.GetValue(this);
                        if (!(exchangableObject is Entity || exchangableObject is IEntity))
                        {
                            throw new Exception($"Data exchange member '_data' in {this.Symbol}  must inherit from {nameof(Entity)} (for structures) or  {nameof(IEntity)} (for function blocks)");
                        }

                        _onliner = exchangableObject;
                    }
                    else
                    {
                        throw new Exception($"Data exchange member '_data' is not member of {this.Symbol}. '_data'  must inherit from {nameof(Entity)} (for structures) or  {nameof(IEntity)} (for function blocks)");
                    }
                }

                return this._onliner;
            }
        }

        private IRepository _repository;

        public IRepository<T> GetRepository<T>() where T : IBrowsableDataObject
            => _repository as IRepository<T>;

        public IRepository GetRepository() => _repository ??
            throw new RepositoryNotInitializedException($"Repository '{Symbol}' is not initialized. You must initialize repository by calling " +
                $"'{nameof(InitializeRepository)}' method with respective parameters.");

        public void InitializeRepository<T>(IRepository repository) where T : IBrowsableDataObject
            => _repository = repository;
        public void InitializeRepository<T>(IRepository<T> repository) where T : IBrowsableDataObject
            => _repository = repository as IRepository;

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            _createTask.InitializeExclusively(Create);
            _readTask.InitializeExclusively(Read);
            _updateTask.InitializeExclusively(Update);
            _deleteTask.InitializeExclusively(Delete);
        }

        private bool Create()
        {
            _createTask.Read();
            Onliner._Id.Synchron = _createTask._identifier.Cyclic;
            var cloned = this.Onliner.CreatePlainerType();
            this.Onliner.FlushOnlineToPlain(cloned);
            try
            {
                _repository.Create(_createTask._identifier.Cyclic, cloned);
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }



        private bool Read()
        {
            _readTask.Read();

            try
            {
                var record = _repository.Read(_readTask._identifier.Cyclic);
                Onliner.FlushPlainToOnline(record);
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        private bool Update()
        {
            _updateTask.Read();
            Onliner._Id.Synchron = _updateTask._identifier.Cyclic;
            var cloned = this.Onliner.CreatePlainerType();
            this.Onliner.FlushOnlineToPlain(cloned);
            try
            {
                _repository.Update(_updateTask._identifier.Cyclic, cloned);
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private bool Delete()
        {
            _deleteTask.Read();
            try
            {
                _repository.Delete(_deleteTask._identifier.Cyclic);
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        private IVortexObject _onlinerVortex;
        protected IVortexObject OnlinerVortex
        {
            get
            {
                if (_onlinerVortex == null)
                {
                    _onlinerVortex = (IVortexObject)Onliner;
                }

                return _onlinerVortex;
            }
        }
    }
}
