﻿using PracticeApplication.Domain.Entities;

namespace PracticeApplication1.Domain.Features.Training
{
    public interface ICourseManagementService
    {
        Task CreateCourseAsync(string title, uint fees, string description);
        Task DeleteCourseAsync(Guid id);
        Task<Course> GetCourseAsync(Guid id);
        Task<(IList<Course> records, int total, int totalDisplay)> GetPagedCoursesAsync(int pageIndex, int pageSize, string searchText, string sortBy);
        Task UpdateCourseAsync(Guid id, string title, string description, double fees);
    }
}