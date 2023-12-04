﻿namespace ShopBee.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        void Save();

    }
}
