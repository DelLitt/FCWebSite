SET IDENTITY_INSERT FCWeb.dbo.Publication ON;

INSERT INTO FCWeb.dbo.Publication
(Id, Title, Header, URLKey, [Image], ShowImageInContet, [Priority], [Visibility], [Enable], articleId, 
imageGalleryId, videoId, userCreated, userChanged, DateCreated, DateChanged, DateDisplayed)
SELECT	ci.contentId AS Id, 
		at.title AS Title,
		at.theHeader AS Header,
		at.outIdName AS URLKey,
		at.ImageUrl AS Image,
		1 AS ShowImageInContet,
		at.priority AS Priority,
		3 AS [Visibility],
		at.enable AS Enable,
		ci.articleId AS articleId,
		ci.photoId AS imageGalleryId,
		ci.videoId AS videoId,
		ci.userCreator AS userCreated,
		ci.userLastChanger AS userChanged,
		ci.dateCreating AS DateCreated,
		ci.dateLastChanging AS DateChanged,
		ci.dateToShow AS DateDisplayed				
FROM SSDBLive.dbo.content_Items AS ci
INNER JOIN SSDBLive.dbo.article_Text AS at ON ci.articleId = at.articleId;

SET IDENTITY_INSERT FCWeb.dbo.Publication OFF;