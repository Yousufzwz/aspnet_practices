using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeApplication.Domain;

public interface IUnitOfWork : IDisposable
{
    void Save();
}