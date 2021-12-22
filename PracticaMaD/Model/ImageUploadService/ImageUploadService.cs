﻿using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public class ImageUploadService : IImageUploadService
    {
        [Inject]
        public IImageUploadDao ImageUploadDao { private get; set; }
        [Inject]
        public ITagDao TagDao { private get; set; }
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }
        [Inject]
        public ICategoryDao CategoryDao { private get; set; }
        [Inject]
        public ITagService TagService { private get; set; }

        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        public long UploadImage(ImageUploadDetails img, List<string> tags, string category)
        {
            ImageUpload image = new ImageUpload();
            image.title = img.title;
            image.descriptions = img.descriptions;
            image.uploadDate = DateTime.Now;
            image.f = img.f;
            image.t = img.t;
            image.iso = img.iso;
            image.wb = img.wb;
            Category categoryObj = new Category();

            if (!(tags==null))
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    try
                    {
                        TagService.CreateTag(tags[i]);
                    }
                    catch (AlreadyCreatedException)
                    {
                    }
                    finally
                    {
                        Tag tag = TagDao.FindByName(tags[i]);
                        if (!(image.Tag.Contains(tag) && tag.ImageUpload.Contains(image)))
                        {
                            image.Tag.Add(tag);
                            //tag.ImageUpload.Add(image);
                            //TagDao.Update(tag);
                        }
                    }

                }
            }

            categoryObj = CategoryDao.FindByName(category);

            categoryObj.ImageUpload.Add(image);
            CategoryDao.Update(categoryObj);
            image.categoryId = categoryObj.categoryId;

            ImageUploadDao.Create(image);

            return image.imgId;
        }

        [Transactional]
        public void UpdatePublication(long imgId, ImageUploadDetails imageDetails)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);

            if (img.Equals(null))
            {
                throw new InstanceNotFoundException(imgId, typeof(long).FullName);
            }

            img.title = imageDetails.title;
            img.descriptions = imageDetails.descriptions;
            img.uploadDate = imageDetails.uploadDate;
            img.f = imageDetails.f;
            img.t = imageDetails.t;
            img.iso = imageDetails.iso;
            img.likes = imageDetails.likes;
            img.wb = imageDetails.wb; 



            ImageUploadDao.Update(img);
        }

        [Transactional]
        public List<ImageUploadDto> SearchByKeywords(string keywords, int startIndex, int count)
        {
            List<ImageUpload> images = ImageUploadDao.FindByTitleOrDescriptionOrCategory(keywords, startIndex, count + 1);
            return ImageUploadConversor.toImageUploadDtos(images);
        }

        [Transactional]
        /// <exception cref="InstanceNotFoundException"/>
        public void RemoveImage(long imgId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);
            ImageUploadDao.Remove(imgId);
        }

        [Transactional]
        public void LikedImage(long imgId, long userId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);
            UserProfile user = UserProfileDao.Find(userId);

            if (img.Equals(null))
            {
                throw new InstanceNotFoundException(imgId, typeof(long).FullName);
            }

            if (user.Equals(null))
            {
                throw new InstanceNotFoundException(userId, typeof(long).FullName);
            }

            if (!(img.UserProfile1.Contains(user) || user.ImageUpload1.Contains(img)))
            {
                img.likes++;
                img.UserProfile1.Add(user);
                ImageUploadDao.Update(img);
                user.ImageUpload1.Add(img);
                UserProfileDao.Update(user);
            }
        }

        [Transactional]
        public void UnlikeImage(long imgId, long userId)
        {
            ImageUpload img = ImageUploadDao.Find(imgId);
            UserProfile user = UserProfileDao.Find(userId);

            if (img.Equals(null))
            {
                throw new InstanceNotFoundException(imgId, typeof(long).FullName);
            }

            if (user.Equals(null))
            {
                throw new InstanceNotFoundException(userId, typeof(long).FullName);
            }

            if ((img.UserProfile1.Contains(user) || user.ImageUpload1.Contains(img)))
            {
                img.likes--;
                img.UserProfile1.Remove(user);
                ImageUploadDao.Update(img);
                user.ImageUpload1.Remove(img);
                UserProfileDao.Update(user);
            }
        }

        [Transactional]

        List<ImageUploadDto> recentUploads(long userId, int startIndex, int count)
        {
            List<ImageUpload> images = ImageUploadDao.FindLastPublications(userId, startIndex, count + 1);
            return ImageUploadConversor.toImageUploadDtos(images);
        }

        List<ImageUploadDto> IImageUploadService.recentUploads(long userId, int startIndex, int count)
        {
            List<ImageUpload> images = ImageUploadDao.FindLastPublications(userId, startIndex, count);
            return ImageUploadConversor.toImageUploadDtos(images);
        }

        public long countComments(long imgId, int startIndex, int count)
        {
            return ImageUploadDao.CountComments(imgId, startIndex, count);
        }

        List<UserProfile> FollowerList(long userId, int startIndex, int count)
        {
            return UserProfileDao.FindFollowers(userId, startIndex, count);
        }

        List<UserProfile> ListOfFollows(long userId, int startIndex, int count)
        {
            return UserProfileDao.FindFollows(userId, startIndex, count);
        }
    }
}
