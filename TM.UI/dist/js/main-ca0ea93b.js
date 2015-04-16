angular.module("underscore",[]).factory("_",function(){return window._});var app=angular.module("TmApp",["ngRoute","LocalStorageModule","angular-loading-bar","ui.bootstrap","underscore","ui.sortable"]);app.config(["$routeProvider",function(e){e.when("/home",{controller:"homeController",templateUrl:"/views/home.html"}),e.when("/login",{controller:"loginController",templateUrl:"/views/login.html"}),e.when("/signup",{controller:"signupController",templateUrl:"/views/signup.html"}),e.when("/boards",{controller:"boardsController",templateUrl:"/views/boards.html"}),e.when("/boards/:boardId",{controller:"boardDetailController",templateUrl:"/views/boardDetail.html"}),e.when("/refresh",{controller:"refreshController",templateUrl:"/views/refresh.html"}),e.when("/tokens",{controller:"tokensManagerController",templateUrl:"/views/tokens.html"}),e.when("/associate",{controller:"associateController",templateUrl:"/views/associate.html"}),e.when("/404",{controller:"404Controller",templateUrl:"/views/404.html"}),e.otherwise({redirectTo:"/home"})}]);var serviceBase="http://tm-api.loc/";app.constant("ngAuthSettings",{apiServiceBaseUri:serviceBase,clientId:"ngTmApp"}),app.config(["$httpProvider",function(e){e.interceptors.push("authInterceptorService")}]),app.run(["authService",function(e){e.fillAuthData()}]),app.factory("authInterceptorService",["$q","$location","localStorageService","$injector",function(e,t,r,o){"use strict";var n={},a=function(e){e.headers=e.headers||{};var t=r.get("authorizationData");return t&&(e.headers.Authorization="Bearer "+t.token),e},s=function(n){if(401===n.status){var a=o.get("authService"),s=r.get("authorizationData");if(s&&s.useRefreshTokens)return t.path("/refresh"),e.reject(n);a.logOut(),t.path("/login")}return 404===n.status&&t.path("/404/"),e.reject(n)};return n.request=a,n.responseError=s,n}]),app.factory("authService",["$http","$q","localStorageService","ngAuthSettings",function(e,t,r,o){"use strict";var n=o.apiServiceBaseUri,a={},s={isAuth:!1,userName:"",useRefreshTokens:!1},i={provider:"",userName:"",externalAccessToken:""},c=function(t){return l(),e.post(n+"api/account/register",t).then(function(e){return e})},u=function(a){var i="grant_type=password&username="+a.userName+"&password="+a.password;a.useRefreshTokens&&(i=i+"&client_id="+o.clientId);var c=t.defer();return e.post(n+"token",i,{headers:{"Content-Type":"application/x-www-form-urlencoded"}}).success(function(e){a.useRefreshTokens?r.set("authorizationData",{token:e.access_token,userName:a.userName,refreshToken:e.refresh_token,useRefreshTokens:!0}):r.set("authorizationData",{token:e.access_token,userName:a.userName,refreshToken:"",useRefreshTokens:!1}),s.isAuth=!0,s.userName=a.userName,s.useRefreshTokens=a.useRefreshTokens,c.resolve(e)}).error(function(e,t){l(),c.reject(e)}),c.promise},l=function(){r.remove("authorizationData"),s.isAuth=!1,s.userName="",s.useRefreshTokens=!1},d=function(){var e=r.get("authorizationData");e&&(s.isAuth=!0,s.userName=e.userName,s.useRefreshTokens=e.useRefreshTokens)},p=function(){var a=t.defer(),s=r.get("authorizationData");if(s&&s.useRefreshTokens){var i="grant_type=refresh_token&refresh_token="+s.refreshToken+"&client_id="+o.clientId;r.remove("authorizationData"),e.post(n+"token",i,{headers:{"Content-Type":"application/x-www-form-urlencoded"}}).success(function(e){r.set("authorizationData",{token:e.access_token,userName:e.userName,refreshToken:e.refresh_token,useRefreshTokens:!0}),a.resolve(e)}).error(function(e,t){l(),a.reject(e)})}return a.promise},h=function(o){var a=t.defer();return e.get(n+"api/account/ObtainLocalAccessToken",{params:{provider:o.provider,externalAccessToken:o.externalAccessToken}}).success(function(e){r.set("authorizationData",{token:e.access_token,userName:e.userName,refreshToken:"",useRefreshTokens:!1}),s.isAuth=!0,s.userName=e.userName,s.useRefreshTokens=!1,a.resolve(e)}).error(function(e,t){l(),a.reject(e)}),a.promise},f=function(o){var a=t.defer();return e.post(n+"api/account/registerexternal",o).success(function(e){r.set("authorizationData",{token:e.access_token,userName:e.userName,refreshToken:"",useRefreshTokens:!1}),s.isAuth=!0,s.userName=e.userName,s.useRefreshTokens=!1,a.resolve(e)}).error(function(e,t){l(),a.reject(e)}),a.promise};return a.saveRegistration=c,a.login=u,a.logOut=l,a.fillAuthData=d,a.authentication=s,a.refreshToken=p,a.obtainAccessToken=h,a.externalAuthData=i,a.registerExternal=f,a}]),app.factory("boardsService",["$http",function(e){"use strict";var t="http://tm-api.loc/",r={},o=function(){return e.get(t+"api/dashboard").then(function(e){return e})},n=function(r){return e.post(t+"api/dashboard/UpdateStatus",r).then(function(e){return e})},a=function(r){return e.post(t+"api/dashboard/add",r).then(function(e){return e})};return r.getBoards=o,r.updateBoardStatus=n,r.addBoard=a,r}]),app.factory("tokensManagerService",["$http","ngAuthSettings",function(e,t){"use strict";var r=t.apiServiceBaseUri,o={},n=function(){return e.get(r+"api/refreshtokens").then(function(e){return e})},a=function(t){return e["delete"](r+"api/refreshtokens/?tokenid="+t).then(function(e){return e})};return o.deleteRefreshTokens=a,o.getRefreshTokens=n,o}]),app.factory("boardDetailService",["$http",function(e){"use strict";var t="http://tm-api.loc/",r={},o=function(r){return e.get(t+"api/boarddetail/"+r).then(function(e){return e})},n=function(r){return e.post(t+"api/boarddetail/addlist",r).then(function(e){return e})},a=function(r,o){var n={listId:r,ids:o};return e.post(t+"api/boarddetail/updatecardsposition",JSON.stringify(n)).then(function(e){return e})},s=function(r){var o={ids:r};return e.post(t+"api/boarddetail/updatelistsposition",JSON.stringify(o)).then(function(e){return e})};return r.getBoard=o,r.addList=n,r.updateCardsPosition=a,r.updateListsPosition=s,r}]),app.controller("indexController",["$scope","$location","authService",function(e,t,r){"use strict";e.logOut=function(){r.logOut(),t.path("/home")},e.authentication=r.authentication}]),app.controller("homeController",["$scope",function(e){"use strict"}]),app.controller("loginController",["$scope","$location","authService","ngAuthSettings",function(e,t,r,o){"use strict";e.loginData={userName:"",password:"",useRefreshTokens:!1},e.message="",e.login=function(){r.login(e.loginData).then(function(e){t.path("/dashboard")},function(t){e.message=t.error_description})},e.authExternalProvider=function(t){var r=location.protocol+"//"+location.host+"/authcomplete.html",n=o.apiServiceBaseUri+"api/Account/ExternalLogin?provider="+t+"&response_type=token&client_id="+o.clientId+"&redirect_uri="+r;window.$windowScope=e;window.open(n,"Authenticate Account","location=0,status=0,width=600,height=750")},e.authCompletedCB=function(o){e.$apply(function(){if("False"==o.haslocalaccount)r.logOut(),r.externalAuthData={provider:o.provider,userName:o.external_user_name,externalAccessToken:o.external_access_token},t.path("/associate");else{var n={provider:o.provider,externalAccessToken:o.external_access_token};r.obtainAccessToken(n).then(function(e){t.path("/dashboard")},function(t){e.message=t.error_description})}})}}]),app.controller("signupController",["$scope","$location","$timeout","authService",function(e,t,r,o){"use strict";e.savedSuccessfully=!1,e.message="",e.registration={userName:"",password:"",confirmPassword:""},e.signUp=function(){o.saveRegistration(e.registration).then(function(t){e.savedSuccessfully=!0,e.message="User has been registered successfully, you will be redicted to login page in 2 seconds.",n()},function(t){var r=[];for(var o in t.data.modelState)for(var n=0;n<t.data.modelState[o].length;n++)r.push(t.data.modelState[o][n]);e.message="Failed to register user due to:"+r.join(" ")})};var n=function(){var e=r(function(){r.cancel(e),t.path("/login")},2e3)}}]),app.controller("boardsController",["$scope","boardsService","$modal",function(e,t,r){"use strict";e.starredBoards=[],e.boards=[],e.userId="",t.getBoards().then(function(t){e.boards=t.data.boards,e.userId=t.data.userId,e.newBoard.userId=t.data.userId,e.updateStarredBoards()},function(e){console.log(e.data.message)}),e.updateStarredBoards=function(){e.starredBoards=_.where(e.boards,{isStarred:!0})},e.updateStarred=function(r,o){o.preventDefault(),t.updateBoardStatus(r.id).then(function(){r.isStarred=!r.isStarred,e.updateStarredBoards()},function(e){console.error(e.data.message)})},e.newBoard={name:"",userId:e.userId},e.resetNewBoard=function(){e.newBoard={name:""}},e.openNewBoard=function(){r.open({templateUrl:"newBoardModal.html",scope:e,controller:["$scope","$modalInstance","newBoard",function(e,r,o){e.newBoard=o,e.submit=function(){t.addBoard(e.newBoard).then(function(t){e.$parent.boards=t.data,e.$parent.updateStarredBoards()},function(e){console.error(e.data.message)}),e.$parent.resetNewBoard(),r.dismiss("cancel")},e.cancel=function(t){t.preventDefault(),e.$parent.resetNewBoard(),r.dismiss("cancel")}}],resolve:{newBoard:function(){return e.newBoard}}})}}]),app.controller("refreshController",["$scope","$location","authService",function(e,t,r){"use strict";e.authentication=r.authentication,e.tokenRefreshed=!1,e.tokenResponse=null,e.refreshToken=function(){r.refreshToken().then(function(t){e.tokenRefreshed=!0,e.tokenResponse=t},function(e){t.path("/login")})}}]),app.controller("tokensManagerController",["$scope","tokensManagerService",function(e,t){"use strict";e.refreshTokens=[],t.getRefreshTokens().then(function(t){e.refreshTokens=t.data},function(e){alert(e.data.message)}),e.deleteRefreshTokens=function(r,o){o=window.encodeURIComponent(o),t.deleteRefreshTokens(o).then(function(t){e.refreshTokens.splice(r,1)},function(e){alert(e.data.message)})}}]),app.controller("associateController",["$scope","$location","$timeout","authService",function(e,t,r,o){"use strict";e.savedSuccessfully=!1,e.message="",e.registerData={userName:o.externalAuthData.userName,provider:o.externalAuthData.provider,externalAccessToken:o.externalAuthData.externalAccessToken},e.registerExternal=function(){o.registerExternal(e.registerData).then(function(t){e.savedSuccessfully=!0,e.message="User has been registered successfully, you will be redicted to orders page in 2 seconds.",n()},function(t){var r=[];for(var o in t.modelState)r.push(t.modelState[o]);e.message="Failed to register user due to:"+r.join(" ")})};var n=function(){var e=r(function(){r.cancel(e),t.path("/dashboard")},2e3)}}]),app.controller("boardDetailController",["$scope","boardDetailService","$routeParams","$modal",function(e,t,r,o){"use strict";e.boardId=r.boardId,e.board={},t.getBoard(e.boardId).then(function(t){e.board=t.data},function(e){console.log(e.data.message)}),e.addCardShow=function(t){_.each(e.board.lists,function(e){e.isAddCardShow=!1}),t.isAddCardShow=!0},e.addCard=function(r){if(r.newCardTitle){var o={boardId:e.boardId,listId:r.id,name:r.newCardTitle};t.addList(o).then(function(e){r.cards=e.data,n(r)},function(e){console.error(e.data.message)})}},e.cancelAddCard=function(e){n(e)};var n=function(e){e.isAddCardShow=!1,e.newCardTitle=""};e.cardsSortableOptions={placeholder:"list-card placeholder",connectWith:".list-cards",start:function(e,t){t.placeholder.height(t.item.height())},stop:function(e,r){var o=r.item.scope().card.listId,n=r.item.sortable.sourceModel,a=_.map(n,function(e){return e.id});t.updateCardsPosition(o,a).then(function(e){},function(e){console.error(e.data.message)})}},e.listsSortableOptions={placeholder:"list placeholder",start:function(e,t){t.placeholder.height(t.item.height())},stop:function(e,r){var o=r.item.sortable.sourceModel,n=_.map(o,function(e){return e.id});t.updateListsPosition(n).then(function(e){},function(e){console.error(e.data.message)})}},e.showDetailCard=function(t){o.open({templateUrl:"detailCardModal.html",scope:e,controller:["$scope","$modalInstance","detailCard",function(e,t,r){}],resolve:{detailCard:function(){return t}}})}}]),app.controller("404Controller",["$scope",function(e){"use strict"}]);