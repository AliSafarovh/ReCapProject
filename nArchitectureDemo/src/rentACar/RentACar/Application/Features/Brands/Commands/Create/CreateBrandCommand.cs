using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logger;
using Core.Application.Pipelines.Transaction;
using Domain.Entites;
using MediatR;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommand : IRequest<CreatedBrandResponse> ,ITransactionalRequest,ICacheRemoverRequest,ILoggableRequest //IRequest Mediatr ile isleyeceni gosterir. 
        //ve emr icra olunduqdan sonra CreatedBrandResponse tipinde netice qaytaracaq.
    {
        public string Name { get; set; } //Yeni brand yaradarken Name-i ozunde saxla

        public string CacheKey => "";

        public bool ByPassCache =>false;
        public string? CacheGroupKey => "GetBrands";
    }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);  //4-try girdikden sonra bunu run edir ilk

            Brand brand = _mapper.Map<Brand>(request);
            brand.Id = Guid.NewGuid();

            //Brand brand2 = _mapper.Map<Brand>(request);
            //brand2.Id = Guid.NewGuid();

            await _brandRepository.AddAsync(brand);//BrandRepository vasitəsilə məlumatları əlavə etmək
            //await _brandRepository.AddAsync(brand2);

            CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(brand);//Mapper ilə Command obyektindən Brand obyektinə çevirmə etmək
            return createdBrandResponse; //Yaradılan Brand obyekti əsasında cavab(response) formalaşdırmaq.
        }
    }

}
