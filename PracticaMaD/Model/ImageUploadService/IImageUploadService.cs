﻿using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageUploadDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService
{
    public interface IImageUploadService
    {
        [Inject]
        IImageUploadDao ImageUploadDao { set; }
        [Inject]
        ITagDao TagDao { set; }
        [Inject]
        ICategoryDao CategoryDao { set; }

        /// <summary>
        /// Upload an image.
        /// </summary>
        ///<param name="ImageUploadDetails"> The image params. </param>
        long UploadImage(ImageUploadDetails img, List<string> tags, string category);

        /// <summary>
        /// Search images by keywords.
        /// </summary>
        ///<param name="keywords"> The search params. </param>
        List<ImageUploadDto> SearchByKeywords(string keywords, int startIndex, int count);

        /// <summary>
        /// Update a publication.
        /// </summary>
        /// <param name="imgId"> The publication id. </param>
        /// <param name="ImageUploadDetails"> The publication details. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void UpdateImage(long pubId, ImageUploadDetails imageDetails);

        /// <summary>
        /// Remove a image.
        /// </summary>
        /// <param name="pubId"> The publication id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void RemoveImage(long imgId);

        /// <summary>
        /// Feedback with the publication.
        /// </summary>
        /// <param name="pubId"> The publication id. </param>
        /// <exception cref="InstanceNotFoundException"/>
        void LikedImage(long imgId, long usrId);

        void UnlikeImage(long imgId, long usrId);

        List<ImageUploadDto> recentUploads(long userId, int startIndex, int count);

        ImageUpload findImage(long imgId);

        List<CommentDto> searchComments(long imgId, int startIndex, int count);

        long countComments(long imgId);

        int getNumberOfImages(long userId);
    }
}
