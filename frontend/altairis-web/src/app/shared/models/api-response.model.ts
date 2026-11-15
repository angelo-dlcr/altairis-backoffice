import { HttpStatusCode } from "@angular/common/http";

export interface ApiResponseBase {
    message: string,
    success: boolean,
    statusCode: HttpStatusCode
}

export interface SingleApiResponse<T> extends ApiResponseBase {
    data?: T
}

export interface ListApiResponse<T> extends ApiResponseBase {
    items?: T[]
}

export interface PagedListApiResponse<T> extends ListApiResponse<T> {
    totalCount: number,
    currentCount: number,
    pageNumber: number,
    pageSize: number,
    totalPages: number,
    pagesArray: number[]
}