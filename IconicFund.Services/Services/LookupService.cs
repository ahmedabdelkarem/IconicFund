using IconicFund.Helpers.Enums;
using IconicFund.Models.Entities;
using IconicFund.Repositories;
using IconicFund.Services.IServices;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace IconicFund.Services.Services
{
    public class LookupService : ILookupService
    {
        //private readonly IBaseRepository repository;
        //private readonly IHasherService hasherService;
        //private readonly IExternalEntityService externalEntityService;
        //private readonly ITransactionType transactionTypeService;
        //private readonly IFoldersService foldersService;
        //private readonly IHardCopyAttachmentsTypesService hardCopyAttachmentsTypesService;

        //public LookupService(IBaseRepository repository, IHasherService hasherService, IExternalEntityService externalEntityService
        //    , ITransactionType transactionTypeService, IFoldersService foldersService , IHardCopyAttachmentsTypesService hardCopyAttachmentsTypesService)
        //{
        //    this.repository = repository;
        //    this.hasherService = hasherService;
        //    this.externalEntityService = externalEntityService;
        //    this.foldersService = foldersService;
        //    this.hardCopyAttachmentsTypesService = hardCopyAttachmentsTypesService;
        //    this.transactionTypeService = transactionTypeService;
        //}

        //public async Task Add(Lookups LookupType, string Name, Guid _UserId)
        //{
        //    switch (LookupType)
        //    {
        //        case Lookups.External:
        //            ExternalEntity ExternalEntity = new ExternalEntity();
        //            ExternalEntity.Name = Name;
        //            bool isExternalEntityNameExist = await externalEntityService.ISExist(Name);
        //            if (!isExternalEntityNameExist)
        //            {
        //                await externalEntityService.Add(ExternalEntity, _UserId);
        //            }
        //            break;

        //        case Lookups.TransactionType:
        //            TransactionType TransactionType = new TransactionType();
        //            TransactionType.Name = Name;
        //            bool isTransactionTypeNameExist = await transactionTypeService.ISExist(Name);
        //            if (!isTransactionTypeNameExist)
        //            {
        //                await transactionTypeService.Add(TransactionType, _UserId);
        //            }
        //            break;

        //        case Lookups.Folders:
        //            Folder Folder = new Folder();
        //            Folder.Name = Name;
        //            bool isFoldersNameExist = await foldersService.ISExist(Name);
        //            if (!isFoldersNameExist)
        //            {
        //                await foldersService.Add(Folder, _UserId);
        //            }
        //            break;

        //        case Lookups.HardCopyAttachmentsTypes:
        //            HardCopyAttachmentsType HardCopy = new HardCopyAttachmentsType();
        //            HardCopy.Name = Name;
        //            bool isHardCopyNameExist = await hardCopyAttachmentsTypesService.ISExist(Name);
        //            if (!isHardCopyNameExist)
        //            {
        //                await hardCopyAttachmentsTypesService.Add(HardCopy, _UserId);
        //            }
        //            break;

        //    }
        //}

        //public async Task<List<ExternalEntity>> GETExternalList() 
        //{
        //    ExternalEntity ExternalEntity = new ExternalEntity();
        //    List<ExternalEntity> ExternalList = repository.GetAll<ExternalEntity>();
        //    return ExternalList;
        //}
        //public async Task<List<TransactionType>> GETTransactionTypeList()
        //{
        //    TransactionType TransactionType = new TransactionType();
        //    List<TransactionType> TransactionTypeList = repository.GetAll<TransactionType>();
        //    return TransactionTypeList;
        //}
        //public async Task<List<Folder>> GETFoldersList()
        //{
        //    Folder Folder = new Folder();
        //    List<Folder> FolderList = repository.GetAll<Folder>();
        //    return FolderList;
        //}
        //public async Task<List<HardCopyAttachmentsType>> GETHardCopyAttachmentsTypesList() 
        //{
        //    HardCopyAttachmentsType HardCopyAttachmentsType = new HardCopyAttachmentsType();
        //    List<HardCopyAttachmentsType> HardCopyAttachmentsTypeList = repository.GetAll<HardCopyAttachmentsType>();
        //    return HardCopyAttachmentsTypeList;
        //}



    }
}
