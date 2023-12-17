//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Sheenam.MVC.Models.Foundations.Hosts;
using Sheenam.MVC.Models.Foundations.Hosts.Exceptions;
using Xeptions;

namespace Sheenam.MVC.Services.Foundations.Hosts
{
    public partial class HostService
    {
        private delegate ValueTask<Host> ReturningHostFunction();

        private async ValueTask<Host> TryCatch(ReturningHostFunction returningHostFunction)
        {
            try
            {
                return await returningHostFunction();
            }
            catch (NullHostException nullHostException)
            {
                throw CreateAndLogValidationException(nullHostException);
            }
            catch (InvalidHostExcpetion invalidHostException)
            {
                throw CreateAndLogValidationException(invalidHostException);
            }
            catch (NotFoundHostException notFoundhostException)
            {
                throw CreateAndLogValidationException(notFoundhostException);
            }
            catch (SqlException sqlException)
            {
                var failedHostStorageException = new FailedHostStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedHostStorageException);
            }
        }

        private HostValidationException CreateAndLogValidationException(Xeption exception)
        {
            var hostValidationExpcetion = new HostValidationException(exception);
            this.loggingbroker.LogError(hostValidationExpcetion);

            return hostValidationExpcetion;
        }
        
        private HostDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var hostDependencyException = new HostDependencyException(exception);
            this.loggingbroker.LogCritical(hostDependencyException);

            return hostDependencyException;
        }
    }
}
