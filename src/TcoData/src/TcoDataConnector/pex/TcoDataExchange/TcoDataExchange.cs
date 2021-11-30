using System;
using System.Linq;
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
                        if (!(exchangableObject is TcoEntity || exchangableObject is ITcoEntity))
                        {
                            throw new Exception($"Data exchange member '_data' in {this.Symbol}  must inherit from {nameof(TcoEntity)}");
                        }

                        _onliner = exchangableObject;
                    }
                    else
                    {
                        throw new Exception($"Data exchange member '_data' is not member of {this.Symbol}. '_data'  must inherit from {nameof(TcoEntity)}");
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


        public void InitializeRemoteDataExchange()
        {
            _createTask.InitializeExclusively(Create);
            _readTask.InitializeExclusively(Read);
            _updateTask.InitializeExclusively(Update);
            _deleteTask.InitializeExclusively(Delete);
            _idExistsTask.InitializeExclusively(Exists);
            _createOrUpdateTask.Initialize(CreateOrUpdate);
        }

        public void InitializeRemoteDataExchange<T>(IRepository<T> repository) where T : IBrowsableDataObject
        {
            this.InitializeRepository(repository);
            this.InitializeRemoteDataExchange();
        }

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
           
        }

        private bool Create()
        {
            _createTask.Read();
            Onliner._EntityId.Synchron = _createTask._identifier.Cyclic;
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


        private bool CreateOrUpdate()
        {
            _createOrUpdateTask.Read();
            var id = _createOrUpdateTask._identifier.LastValue;
            Onliner._EntityId.Synchron = id;
            if (!this._repository.Exists(id))
            {                
                var cloned = this.Onliner.CreatePlainerType();
                this.Onliner.FlushOnlineToPlain(cloned);
                try
                {
                    _repository.Create(id, cloned);
                    return true;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            else
            {
                var cloned = this.Onliner.CreatePlainerType();
                this.Onliner.FlushOnlineToPlain(cloned);
                _repository.Update(id, cloned);
                return true;
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
            Onliner._EntityId.Synchron = _updateTask._identifier.Cyclic;
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

        private bool Exists()
        {
            _idExistsTask.Read();
            try
            {
                _idExistsTask._exists.Synchron = _repository.Exists(_idExistsTask._identifier.Cyclic);                
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
