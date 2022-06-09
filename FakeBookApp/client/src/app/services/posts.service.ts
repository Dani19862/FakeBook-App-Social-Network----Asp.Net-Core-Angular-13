import { CommentService } from './comment.service';
import { map, observable, Observable, tap } from 'rxjs';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { Post } from '../models/post';
import { PaginatedResult } from '../models/pagination';
import { PostParams } from '../models/postParams';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  baseUrl = environment.apiUrl;
  //posts : Observable <Post[]>;
  posts: Post[] = [];
  post: Post
  comments: Comment[] = [];
  postParams : PostParams
  paginatedResult: PaginatedResult<Post[]> = new PaginatedResult<Post[]>();



  constructor(private http: HttpClient) { }


  // getallPosts(page?:number, itemsPerPage?:number) : Observable <PaginatedResult<Post[]>> {

    //   let params = new HttpParams();
    //   if(page != null && itemsPerPage != null){
  //     params = params.append('pageNumber', page.toString());
  //     params = params.append('pageSize', itemsPerPage.toString());
  //   }
  //   return this.http.get<Post[]>(`${this.baseUrl}`, {
  //     observe: 'response',
  //     params
  //   }).pipe(
    //     map((res: HttpResponse<Post[]>) => {
      //       this.paginatedResult.result = res.body as Post[];
      //       if(res.headers.get('Pagination') != null){
        //         this.paginatedResult.pagination = JSON.parse(res.headers.get('Pagination')|| '');
        //       }
        //       return this.paginatedResult;
        //     })
        //   );

        // }

  // get all posts
  getallPosts() {
    // return this.http.get<Post[]>(`${this.baseUrl}post`).pipe(
    //   tap((posts : Observable<Post[]> | any) => this.posts = posts))
    //   .pipe(map(posts => {
    //     return posts.map((post :Post| any) => {
    //       post.comments = this.commentService.getComments(post.id);
    //       return post;
    //     }
    //     );}
    // ));


    // return this.http.get<Post[]>(`${this.baseUrl}post`).pipe(
    //   tap(posts => this.posts = posts)).subscribe(posts => {
    //     return posts.map((post: Observable <Post[]> | any) => {
    //       post.comments = this.commentService.getComments(post.id);
    //       return post;
    //     }
    //     );}
    // );

    return this.http.get<Post[]>(`${this.baseUrl}Post`).pipe(
      tap(posts => this.posts = posts));

  }

 // create new post
  createPost(post: Post) : Observable<Post> {
    return this.http.post<Post>(`${this.baseUrl}Post`, post);
  }


  // get all users posts
   getUsersPosts(username: string)  {
    return this.http.get<Post[]>(`${this.baseUrl}Post/${username}`).pipe(
      tap((posts : Observable<Post[]> | any) => this.posts = posts));
    }

    // delete post
    deletePost(postId: number) : Observable<any> {
      return this.http.delete(`${this.baseUrl}Post/delete-post/${postId}`);
    }

  // update Post
   updatePost(post: Post) {
   return this.http.put(`${this.baseUrl}post`, post);

  }
  getPost(id: number) {
    return this.http.get<Post>(`${this.baseUrl}post/${id}`);
  }

}
