import { Text, View, TextInput, Button, Alert } from "react-native";
import {
  useForm,
  Controller,
  Control,
  FieldErrors,
  UseFormHandleSubmit,
} from "react-hook-form";
import { GenericObjectType } from "../../types/GenericObjectType";
import FormError from "./FormError";
import ChildrenWithProps from "../../utils/ChildrenWithProps";
import { createContext, useEffect } from "react";

interface FormProps {
  children: React.ReactNode;
  defaultValues?: GenericObjectType;
  onSubmit?: (data: GenericObjectType) => void;
}

type FormContextType = {
  control: Control<GenericObjectType, any>;
  errors: FieldErrors<GenericObjectType>;
  submit: (
    e?: React.BaseSyntheticEvent<object, any, any> | undefined
  ) => Promise<void>;
};

export const FormContext = createContext({} as FormContextType);

export default function Form({ children, defaultValues, onSubmit }: FormProps) {
  const {
    control,
    handleSubmit,
    formState: { errors },
    register,
  } = useForm({
    defaultValues: defaultValues,
  });

  const sendSubmit = (data: GenericObjectType) => {
    if (!!onSubmit) onSubmit(data);
  };

  const submit = handleSubmit(sendSubmit);

  return (
    <FormContext.Provider value={{ control, errors, submit }}>
      {children}
    </FormContext.Provider>
  );
}
