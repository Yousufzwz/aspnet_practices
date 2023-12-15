using PracticeApplication.Application;
using PracticeApplication.Domain.Entities;
using PracticeApplication1.Domain.Features.Training;
using PracticeApplication1.Domain.Features.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeApplication1.Application.Features.Training;

public class CourseManagementService : ICourseManagementService
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    public CourseManagementService(IApplicationUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    
    public void CreateCourse(string title, uint fees, string description)
    {
        Course course = new Course
        {
            Title = title,
            Fees = fees,
            Description = description
        };
        
        _unitOfWork.CourseRepository.Add(course);
        _unitOfWork.Save();


    }
}
