import React from "react";
import { View } from "react-native";
import DefaultScreenStructure from "../DefaultScreenStructure";
import BuildForm from "../Form/BuildForm";
import { BuildInputsType } from "../Form/BuildInputs";
import { Stack } from "expo-router";
import Spacing from "../../constants/Spacing";
import { GenericObjectType } from "../../types/GenericObjectType";

interface FormScreenStructureProps {
  buttonText?: string;
  title: string;
  inputs: BuildInputsType;
  onSubmit?: (data: GenericObjectType) => void;
}

const FormScreenStructure: React.FC<FormScreenStructureProps> = ({
  buttonText,
  inputs,
  title,
  onSubmit,
}) => {
  return (
    <>
      <Stack.Screen options={{ headerShown: false }} />
      <DefaultScreenStructure title={title} activeBackButton>
        <View style={{ padding: Spacing.g }}>
          <BuildForm
            inputs={inputs}
            buttonText={buttonText}
            onSubmit={onSubmit}
          />
        </View>
      </DefaultScreenStructure>
    </>
  );
};

export default FormScreenStructure;
