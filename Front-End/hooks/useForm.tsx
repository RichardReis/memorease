import { useContext } from 'react';
import { FormContext } from '../components/Form';

export function useForm() {
  const form = useContext(FormContext);
  return form;
}
