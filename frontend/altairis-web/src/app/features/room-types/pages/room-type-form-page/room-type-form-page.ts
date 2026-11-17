import { Component, DestroyRef, effect, inject, input, linkedSignal, output } from '@angular/core';
import { HotelApiService } from '../../../hotels/services/hotel-service';
import { ToastService } from '../../../../shared/components/toast-component/service/toast-service';
import { RoomTypeApiService } from '../../services/room-type-service';
import { CreateRoomTypeRequest, ROOM_TYPE_CODES, RoomType, RoomTypeCodeLabels, UpdateRoomTypeRequest } from '../../models/room-type.model';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { rxResource, takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { HttpStatusCode } from '@angular/common/http';
import { Hotel } from '../../../hotels/models/hotel.model';
import { of } from 'rxjs';
import { ListApiResponse } from '../../../../shared/models/api-response.model';
import { DropdownSelectComponent } from '../../../../shared/components/dropdown-select-component/dropdown-select-component';

@Component({
  imports: [
    ReactiveFormsModule,
    DropdownSelectComponent,
],
  selector: 'app-room-type-form-page',
  styleUrl: './room-type-form-page.scss',
  templateUrl: './room-type-form-page.html',
})
export class RoomTypeFormPageComponent {
  private apiService = inject(RoomTypeApiService);
  private hotelApiService = inject(HotelApiService);
  private destroyRef = inject(DestroyRef);
  private toastService = inject(ToastService);

  roomTypeCodes = ROOM_TYPE_CODES;
  readonly roomTypeCodeLabels = RoomTypeCodeLabels;

  isModalOpen = input<boolean>(false);
  
  saved = output<boolean>();
  close = output<void>();
  isFormValid = output<boolean>();

  editing = input.required<boolean>();
  viewing = input.required<boolean>();
  currentRoomType = input.required<RoomType | undefined | null>()
  
  loading = output<boolean>();

  createRequest: CreateRoomTypeRequest = {
    hotelId: 0,
    code: '',
    name: '',
    description: null,
    maxOccupancy: 0
  }
  
  updateRequest: UpdateRoomTypeRequest = {
    id: 0,
    hotelId: 0,
    code: '',
    name: '',
    description: null,
    maxOccupancy: 0,
    isActive: true
  }

  form = new FormGroup({
    hotelId: new FormControl(0, {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.min(1)
      ]
    }),

    code: new FormControl('', {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.maxLength(30)
      ]
    }),

    name: new FormControl('', {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.maxLength(150)
      ]
    }),

    description: new FormControl('', {
      nonNullable: false,
      validators: [
        Validators.maxLength(100)
      ]
    }),

    maxOccupancy: new FormControl(0, {
      nonNullable: true,
      validators: [
        Validators.required,
        Validators.min(1)
      ]
    }),

    isActive: new FormControl(true, {
      nonNullable: true
    })
  });

  constructor() 
  {
    effect(() => {
      // this.hotelResource.reload();
      
      const isOpen = this.isModalOpen();
      const roomType = this.currentRoomType();
      const isEditing = this.editing();
      const isViewing = this.viewing();

      this.form.enable({ emitEvent: false });

      if (roomType) {
        this.form.patchValue({
          hotelId: roomType.hotelId,
          code: roomType.code,
          name: roomType.name,
          description: roomType.description,
          maxOccupancy: roomType.maxOccupancy,
          isActive: roomType.isActive,
        }, 
        { 
          emitEvent: false
        });
      } else {
        this.resetForm();
      }

      if (isViewing) {
        this.form.disable();
      } 
      else {
        if (isEditing) {
          this.form.controls.isActive.enable();
        } 
        else {
          this.form.controls.isActive.disable();
        }
      }

      this.isFormValid.emit(this.form.valid);
    });

    if(this.error()) {
      this.toastService.show(
        'Error del servidor: No se pudo cargar el listado de hoteles',
        'danger',
      )
    }

    this.form.statusChanges
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        this.isFormValid.emit(this.form.valid);
      });
  }
      
  get codeControl(): FormControl<string> {
    return this.form.controls.code;
  }


  hotelResource = rxResource({
    params: () => ({ 
      isOpen: this.isModalOpen() 
    }),
    stream: ({params}) => {
      return (params.isOpen)
        ? this.hotelApiService.getAllActive()
        : of({ data: [] } as unknown as ListApiResponse<Hotel>);
    }
  });

  hotels = this.hotelResource.value;
  error = this.hotelResource.error;


  validateNumber(event: KeyboardEvent) {
    const teclasProhibidas = ['e', 'E', '+', '-', '.', ','];
    
    if (teclasProhibidas.includes(event.key)) {
      event.preventDefault();
    }
  }

  
  onSubmit(): void {
    if (this.editing()) {
      this.processUpdate();
    }
    else {
      this.processCreate();
    }
  }

  onClose(): void {
    this.form.markAsUntouched();
    this.form.clearValidators();
    this.resetForm();
    this.close.emit();
  }

  resetForm = () => {
      this.form.reset({
          hotelId: 0,
          code: '',
          name: '',
          description: '',
          maxOccupancy: 0,
          isActive: true
      });
  };
  
  processCreate(): void {
    this.loading.emit(true);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
    }
    else {
      this.createRequest.hotelId = this.form.controls.hotelId.value;
      this.createRequest.code = this.form.controls.code.value;
      this.createRequest.name = this.form.controls.name.value;
      this.createRequest.description = this.form.controls.description.value;
      this.createRequest.maxOccupancy = this.form.controls.maxOccupancy.value;

      this.apiService.create(this.createRequest).subscribe({
        next: (response) => {
          if (response.success) {
            this.saved.emit(response.data!)
            
            this.toastService.show(
              response.message ?? 'Registro creado exitosamente.',
              'success',
            )

            this.onClose();
          }
        },
        error: (err) => {
          if(err.status === HttpStatusCode.BadRequest)
          {
            this.toastService.show(
              'No se puede registar un código repetido en un mismo hotel.',
              'warning',
            )
          }
          else {
            this.toastService.show(
              'Error del servidor: ' + err?.error?.message,
              'danger',
            )
          }

          this.loading.emit(false);
        },
        complete: () => {
          this.loading.emit(false);
        }
      })
    }
  }

  processUpdate(): void {

  }
}
