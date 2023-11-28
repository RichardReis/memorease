import React from "react";
import Form from "..";
import BuildInputs, { BuildInputsType } from "../BuildInputs";
import SubmitButton from "../SubmitButton";
import { GenericObjectType } from "../../../types/GenericObjectType";

interface BuildFormProps {
  buttonText?: string;
  defaultValues?: GenericObjectType;
  inputs: BuildInputsType;
  onSubmit?: (data: GenericObjectType) => void;
}

const BuildForm: React.FC<BuildFormProps> = ({
  buttonText,
  defaultValues,
  inputs,
  onSubmit,
}) => {
  return (
    <Form defaultValues={defaultValues} onSubmit={onSubmit}>
      <BuildInputs inputs={inputs} />
      <SubmitButton title={buttonText ?? "Enviar"} />
    </Form>
  );
};

export default BuildForm;
