﻿using PracticeApplication.Application;
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

    
    public async Task CreateCourseAsync(string title, uint fees, string description)
    {
        bool isDuplicateTitle = await _unitOfWork.CourseRepository.
            IsTitleDuplicateAsync(title);

        if (isDuplicateTitle)
            throw new InvalidOperationException();
        
            Course course = new Course
            {
                Title = title,
                Fees = fees,
                Description = description
            };

            _unitOfWork.CourseRepository.Add(course);
            await _unitOfWork.SaveAsync();
    }

    public async Task DeleteCourseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Course> GetCourseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<(IList<Course> records, int total, int totalDisplay)> GetPagedCoursesAsync(int pageIndex, int pageSize, string searchText, string sortBy)
    {
        return await _unitOfWork.CourseRepository.GetTableDataAsync(searchText, sortBy, 
            pageIndex, pageSize);
    }

    public async Task UpdateCourseAsync(Guid id, string title, string description,
           double fees)
    {
        throw new NotImplementedException();
    }
}
