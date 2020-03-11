var currentNews;

$(document).ready(function () {
    $("#divSearch").hide();
    loadAllFeeds();

    $("#btnAddFeed").click(addFeed);
    $("#txtSearch").keydown(searchNews);
    $("#drpOrder").change(changeSort);
});

function addFeed() {
    var txtCategory = $("#txtCategory");
    var txtNewsFeedName = $("#txtNewsFeedName");
    var txtNewsFeedUrl = $("#txtNewsFeedUrl");
    var hasError = false;

    if (txtCategory.val().trim() === "") {
        $("#groupCategory").addClass("has-error");
        hasError = true;
    }
    if (txtNewsFeedName.val().trim() === "") {
        $("#groupNewsFeedName").addClass("has-error");
        hasError = true;
    }
    if (txtNewsFeedUrl.val().trim() === "") {
        $("#groupNewsFeedUrl").addClass("has-error");
        hasError = true;
    }

    if (!hasError) {
        toggleLoading();
        $.post("api/NewsFeed", {
            CategoryName: txtCategory.val().trim(),
            NewsFeedName: txtNewsFeedName.val().trim(),
            NewsFeedURL: txtNewsFeedUrl.val().trim()
        }, function () {
            txtCategory.val("");
            txtNewsFeedName.val("");
            txtNewsFeedUrl.val("");
            loadAllFeeds();
        }).fail(function (err) {
            alert(err.responseJSON.ExceptionMessage);
            toggleLoading();
        }).always(function () {
        });
    }
}

function searchNews(e) {
    if (e.keyCode === 13) {
        let searchText = this.value;
        createNewsHtml(currentNews.filter(o => o.Title.indexOf(searchText) !== -1 || o.Summary.indexOf(searchText) !== -1));
    }
}

function changeSort() {
    createNewsHtml(currentNews);
}

function toggleLoading() {
    $("#divLoading").toggle();
}

function loadAllFeeds() {
    document.getElementById("divNewsFeeds").innerHTML = "";
    document.getElementById("divYourFeeds").innerHTML = "";
    $.get("api/Category", function (data) {
        createNewsFeeds(data);
        toggleLoading();
    });
}

function createNewsFeeds(categories) {
    var notSubscribedFeeds = categories.filter(category => category.NewsFeeds.filter(feed => !feed.Subscribed).length);
    if (notSubscribedFeeds.length) {
        document.getElementById("divNewsFeeds").appendChild(createList(notSubscribedFeeds, false));
    }

    var subscribedFeeds = categories.filter(category => category.NewsFeeds.filter(feed => feed.Subscribed).length);
    if (subscribedFeeds.length) {
        var divYourFeeds = document.getElementById("divYourFeeds");

        var AllNewsLink = document.createElement("A");
        AllNewsLink.href = "#";
        AllNewsLink.innerText = "All news";
        AllNewsLink.addEventListener("click", this.showAllNews);

        divYourFeeds.appendChild(AllNewsLink, false);
        divYourFeeds.appendChild(createList(subscribedFeeds, true));
    }
}

function createList(categories, subscribed) {
    var mainUL = document.createElement("UL");
    for (var i = 0; i < categories.length; i++) {
        var category = categories[i];
        var categoryLI = document.createElement("LI");
        var categoryUL = document.createElement("UL");

        categoryLI.className = "category-name";
        if (subscribed) {
            categoryLI.appendChild(createCategoryNameLink(category));
        } else {
            categoryLI.append(category.Description);
        }

        var feeds = category.NewsFeeds.filter(feed => feed.Subscribed === subscribed);

        for (var j = 0; j < feeds.length; j++) {
            var feed = feeds[j];
            var feedLI = document.createElement("LI");

            feedLI.appendChild(createFeedAddRemoveLink(feed));

            if (feed.Subscribed)
                feedLI.appendChild(createFeedNameLink(feed));
            else
                feedLI.append(feed.FeedName);

            categoryUL.appendChild(feedLI);
        }

        categoryLI.appendChild(categoryUL);
        mainUL.appendChild(categoryLI);
    }
    return mainUL;
}

function createFeedAddRemoveLink(feed) {
    var feedAddRemoveLink = document.createElement("A");
    feedAddRemoveLink.href = "#";
    feedAddRemoveLink.innerText = feed.Subscribed ? " - " : " + ";
    feedAddRemoveLink.setAttribute("title", (feed.Subscribed ? "Remove" : "Add") + " news");
    feedAddRemoveLink.addEventListener("click", this.toggleFeedSubscribed.bind(null, feed.ID));
    return feedAddRemoveLink;
}

function createFeedNameLink(feed) {
    var feedNameLink = document.createElement("A");
    feedNameLink.href = "#";
    feedNameLink.innerText = feed.FeedName;
    feedNameLink.setAttribute("title", "Show news");
    feedNameLink.addEventListener("click", this.showNewsByFeed.bind(null, feed.ID));
    return feedNameLink;
}

function createCategoryNameLink(category) {
    var CategoryNameLink = document.createElement("A");
    CategoryNameLink.href = "#";
    CategoryNameLink.innerText = category.Description;
    CategoryNameLink.setAttribute("title", "Show all news");
    CategoryNameLink.addEventListener("click", this.showNewsByCategory.bind(null, category.ID));
    return CategoryNameLink;
}

function toggleFeedSubscribed(feedID) {
    toggleLoading();
    $.post("api/NewsFeed/ToggleSubscribed/" + feedID,
        function () {
            loadAllFeeds();
        }).fail(function (err) {
            alert(err.responseJSON.ExceptionMessage);
            toggleLoading();
        });
}

function showNewsByFeed(feedID) {
    loadNews("api/NewsFeedItems/" + feedID);
}

function showNewsByCategory(categoryID) {
    loadNews("api/NewsFeedItems/Category/" + categoryID);
}

function showAllNews() {
    loadNews("api/NewsFeedItems");
}

function loadNews(url) {
    toggleLoading();
    $.get(url,
        function (data) {
            currentNews = data;
            showDivSearch(data.length);
            createNewsHtml(data);
        }).fail(function (err) {
            alert(err.responseJSON.ExceptionMessage);
        }).always(function () {
            toggleLoading();
        });
}

function createNewsHtml(newsFeedItems) {
    var divAllNews = document.getElementById("divNews");
    divAllNews.innerHTML = "";
    newsFeedItems = sortNews(newsFeedItems);
    for (var i = 0; i < newsFeedItems.length; i++) {
        var newsFeedItem = newsFeedItems[i];
        var divNews = document.createElement("DIV");
        var titleH2 = document.createElement("H2");

        var titleLink = document.createElement("A");
        titleLink.href = newsFeedItem.ItemUrl;
        titleLink.target = "_blank";
        titleLink.innerText = newsFeedItem.Title;

        titleH2.appendChild(titleLink);
        divNews.appendChild(titleH2);

        var authorP = document.createElement("P");
        authorP.innerText = newsFeedItem.Date + " - " + newsFeedItem.Creator;
        divNews.appendChild(authorP);

        var summaryP = document.createElement("P");
        summaryP.innerText = newsFeedItem.Summary;
        divNews.appendChild(summaryP);

        divAllNews.appendChild(divNews);
    }
}

function showDivSearch(show) {
    if (show)
        $("#divSearch").show();
    else
        $("#divSearch").hide();
}

function sortNews(newsFeedItems) {
    var sortBy = $("#drpOrder").val();
    newsFeedItems.sort(function (obj, obj2) {
        if (sortBy === "Date") {
            var date1 = new Date(obj.Date);
            var date2 = new Date(obj2.Date);
            if (date1 < date2) return -1;
            else if (date1 > date2) return +1;
            else return 0;
        } else {
            if (obj[sortBy] < obj2[sortBy]) return -1;
            else if (obj[sortBy] > obj2[sortBy]) return +1;
            else return 0;
        }
    });
    return newsFeedItems;
}