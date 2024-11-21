import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusFormatter'
})
export class StatusFormatterPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
