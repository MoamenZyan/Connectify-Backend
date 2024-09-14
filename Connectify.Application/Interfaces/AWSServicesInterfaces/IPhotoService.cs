using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Application.Interfaces.AWSServicesInterfaces
{
    public interface IPhotoService
    {
        Task<string> UploadPhoto(IFormFile photo, Guid userId);
        Task<bool> DeletePhoto(string photoPath);
    }
}
