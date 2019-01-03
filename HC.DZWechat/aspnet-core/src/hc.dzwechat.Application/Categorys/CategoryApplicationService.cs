
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using HC.DZWechat.Categorys;
using HC.DZWechat.Categorys.Dtos;
using HC.DZWechat.Categorys.DomainService;



namespace HC.DZWechat.Categorys
{
    /// <summary>
    /// Category应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class CategoryAppService : DZWechatAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category, int> _entityRepository;

        private readonly ICategoryManager _entityManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public CategoryAppService(
        IRepository<Category, int> entityRepository
        , ICategoryManager entityManager
        )
        {
            _entityRepository = entityRepository;
            _entityManager = entityManager;
        }


        /// <summary>
        /// 获取Category的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<CategoryListDto>> GetPaged(GetCategorysInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<CategoryListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<CategoryListDto>>();

            return new PagedResultDto<CategoryListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 获取商品类型
        /// </summary>
        public async Task<List<CategoryTreeNode>> GetCategoryTreesAsync()
        {
            var category = await _entityRepository.GetAll().OrderBy(v => v.Seq).AsNoTracking()
             .Select(v => new CategoryListDto() { Id = v.Id, Name = v.Name }).ToListAsync();
            List<CategoryTreeNode> list = new List<CategoryTreeNode>();
            CategoryTreeNode treeNodeRoot = new CategoryTreeNode();
            treeNodeRoot.title = "全部";
            treeNodeRoot.key = "root";
            treeNodeRoot.Expanded = true;
            treeNodeRoot.children = new List<NzTreeNode>();
            if (category.Count > 0)
            {
                foreach (var item in category)
                {
                    NzTreeNode treeNode = new NzTreeNode()
                    {
                        key = item.Id.ToString(),
                        title = item.Name.ToString(),
                        IsLeaf = true,
                    };
                    treeNodeRoot.children.Add(treeNode);
                }
            }
            list.Add(treeNodeRoot);
            return list;
        }

        /// <summary>
        /// 通过指定id获取CategoryListDto信息
        /// </summary>

        public async Task<CategoryListDto> GetById(EntityDto<int> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<CategoryListDto>();
        }

        /// <summary>
        /// 获取编辑 Category
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetCategoryForEditOutput> GetForEdit(NullableIdDto<int> input)
        {
            var output = new GetCategoryForEditOutput();
            CategoryEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

                editDto = entity.MapTo<CategoryEditDto>();

                //categoryEditDto = ObjectMapper.Map<List<categoryEditDto>>(entity);
            }
            else
            {
                editDto = new CategoryEditDto();
            }

            output.Category = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Category的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<CategoryListDto> CreateOrUpdate(CategoryEditDto input)
        {

            if (input.Id.HasValue)
            {
                return await Update(input);
            }
            else
            {
                return await Create(input);
            }
        }


        /// <summary>
        /// 新增Category
        /// </summary>

        protected virtual async Task<CategoryListDto> Create(CategoryEditDto input)
        {
            var entity = input.MapTo<Category>();
            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<CategoryListDto>();
        }

        /// <summary>
        /// 编辑Category
        /// </summary>

        protected virtual async Task<CategoryListDto> Update(CategoryEditDto input)
        {
            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            await _entityRepository.UpdateAsync(entity);
            return entity.MapTo<CategoryListDto>();
        }



        /// <summary>
        /// 删除Category信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Category的方法
        /// </summary>

        public async Task BatchDelete(List<int> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 获取商品类型Select
        /// </summary>
        public async Task<List<SelectGroup>> GetCategorySelectGroup()
        {
            var entity = await (from pt in _entityRepository.GetAll()
                                select new
                                {
                                    text = pt.Name,
                                    value = pt.Id,
                                    seq = pt.Seq
                                }).OrderBy(v => v.seq).AsNoTracking().ToListAsync();
            return entity.MapTo<List<SelectGroup>>();
        }
    }
}


