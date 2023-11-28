import React from "react";
import { Text, View, StyleSheet } from "react-native";
import { Controller } from "react-hook-form";
import FormError from "../FormError";
import { useForm } from "../../../hooks/useForm";
import InputText from "../../Inputs/InputText";
import TextStyles from "../../../themedStyles/Text";
import Spacing from "../../../constants/Spacing";

interface BuildInputsProps {
  inputs: BuildInputsType;
}

export type BuildInputType = {
  name: string;
  label?: string;
  placeholder?: string;
  required: boolean;
  type: "text" | "password";
};

export type BuildInputsType = BuildInputType[];

const BuildInputs: React.FC<BuildInputsProps> = ({ inputs }) => {
  const { control, errors } = useForm();

  return (
    <>
      {inputs.map((input, i) => (
        <View key={`input-${i}`} style={styles.container}>
          {input.label && (
            <Text style={{ ...TextStyles.label }}>{input.label}</Text>
          )}
          <Controller
            control={control}
            rules={{
              required: input.required,
            }}
            render={({ field: { onChange, onBlur, value } }) => (
              <InputText
                placeholder={input.placeholder}
                onBlur={onBlur}
                onChange={onChange}
                value={value}
                type={input.type}
              />
            )}
            name={input.name}
          />
          {errors && <FormError show={!!errors[input.name]} />}
        </View>
      ))}
    </>
  );
};

const styles = StyleSheet.create({
  container: {
    width: "100%",
    marginBottom: Spacing.g,
  },
});

export default BuildInputs;
