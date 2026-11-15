import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const apiErrorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {

      console.error('API Request Error:', {
        url: req.url,
        status: error.status,
        message: error.message,
        error: error.error
      });

      if (error.status === 0) {
        console.error(
          'Unable to connect to the API. Check whether the backend is running and CORS is configured.'
        );
      }

      return throwError(() => error);
    })
  );
};