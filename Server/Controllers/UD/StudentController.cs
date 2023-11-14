using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
	public class StudentController : BaseController, GenericRestController<StudentDTO>
    {
		public StudentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
		{
		}


        [HttpDelete]
        [Route("Delete/{StudentID}")]
        public async Task<IActionResult> Delete(int StudentID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == StudentID).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Students.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Students.Select(sp => new StudentDTO
                {
                    StudentId = sp.StudentId,
                    Zip = sp.Zip,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Employer = sp.Employer,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    Phone = sp.Phone,
                    RegistrationDate = sp.RegistrationDate,
                    Salutation = sp.Salutation,
                    StreetAddress = sp.StreetAddress
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get/{StudentID}")]
        public async Task<IActionResult> Get(int StudentID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                StudentDTO? result = await _context
                    .Students
                    .Where(x => x.StudentId == StudentID)
                     .Select(sp => new StudentDTO
                     {
                         StudentId = sp.StudentId,
                         Zip = sp.Zip,
                         CreatedBy = sp.CreatedBy,
                         CreatedDate = sp.CreatedDate,
                         FirstName = sp.FirstName,
                         LastName = sp.LastName,
                         ModifiedBy = sp.ModifiedBy,
                         ModifiedDate = sp.ModifiedDate,
                         Phone = sp.Phone,
                         RegistrationDate = sp.RegistrationDate,
                         Salutation = sp.Salutation,
                         StreetAddress = sp.StreetAddress,
                          Employer= sp.Employer
                     })
                .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]
                                                StudentDTO _StudentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == _StudentDTO.StudentId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Student s = new Student
                    {
                        StudentId = _StudentDTO.StudentId,
                        Salutation = _StudentDTO.Salutation,
                        StreetAddress = _StudentDTO.StreetAddress,
                        FirstName = _StudentDTO.FirstName,
                        LastName = _StudentDTO.LastName,
                        Zip = _StudentDTO.Zip,
                        Phone = _StudentDTO.Phone,
                        Employer = _StudentDTO.Employer,
                        RegistrationDate = _StudentDTO.RegistrationDate
                    };
                    _context.Students.Add(s);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }


        
        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == _StudentDTO.StudentId).FirstOrDefaultAsync();
                itm.Employer = _StudentDTO.Employer;
                itm.FirstName = _StudentDTO.FirstName;
                itm.LastName = _StudentDTO.LastName;
                itm.Phone = _StudentDTO.Phone;
                itm.RegistrationDate = _StudentDTO.RegistrationDate;
                itm.Salutation = _StudentDTO.Salutation;
                itm.StreetAddress = _StudentDTO.StreetAddress;
                itm.Zip = _StudentDTO.Zip;

                _context.Students.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
    }
}

