using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.DataContracts.Models;
using NSI.DataContracts.Request;
using NSI.REST.Filters;
using NSI.REST.Helpers;

namespace NSI.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeManipulation _employeeManipulation;

        public EmployeeController(IEmployeeManipulation employeeManipulation)
        {
            _employeeManipulation = employeeManipulation;
        }

        /// <summary>
        /// Gets all employees data (admin only).
        /// </summary>
        [Authorize]
        [PermissionCheck("employee:view")]
        [HttpGet]
        public BaseResponse<List<User>> GetAllEmployees()
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<List<User>>
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<List<User>>
            {
                Data = _employeeManipulation.GetAllEmployees(),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }

        /// <summary>
        /// Creates new employee (admin only).
        /// </summary>
        [Authorize]
        [PermissionCheck("employee:create")]
        [HttpPost]
        public BaseResponse<User> SaveEmployee(NewEmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResponse<User>
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            if (request.FirstName == null || request.LastName == null || request.Username == null ||
                !new EmailAddressAttribute().IsValid(request.Email) || request.Email == null ||
                request.PlaceOfBirth == null || request.Country == null)
            {
                return new BaseResponse<User>
                {
                    Data = null,
                    Error = ValidationHelper.ToErrorResponse(ModelState),
                    Success = ResponseStatus.Failed
                };
            }

            return new BaseResponse<User>
            {
                Data = _employeeManipulation.SaveEmployee(request),
                Error = ValidationHelper.ToErrorResponse(ModelState),
                Success = ResponseStatus.Succeeded
            };
        }
    }
}
