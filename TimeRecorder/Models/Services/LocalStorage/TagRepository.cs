﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecorder.Models.DTOs;

namespace TimeRecorder.Models.Services.LocalStorage
{
    public class TagRepository : AbstractCrudRepo<TagDTO>, ITagRepository
    {
        public TagRepository(ITimeRecorderContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TagDTO>> FindByNameAsync(string name)
        {
            await Task.FromResult(0);
            var set = context.Set<TagDTO>();
            return set.Where(tag => tag.TagValue == name);
        }
    }
}
