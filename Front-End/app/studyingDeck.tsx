import React, { useState, useEffect } from "react";
import { View, StyleSheet } from "react-native";
import FlashCard from "../components/FlashCard";
import Colors from "../constants/Colors";
import { Stack, useLocalSearchParams, useRouter } from "expo-router";
import Constants from "expo-constants";
import Button from "../components/Button";
import Spacing from "../constants/Spacing";
import { LoadStudy, LoadStudyData } from "../service/StudyDeck/loadStudy";
import { SaveStudy } from "../service/StudyDeck/saveStudy";

const StudyingDeck: React.FC = () => {
  const router = useRouter();
  const params = useLocalSearchParams();
  const { deckId } = params;

  const [data, setData] = useState<LoadStudyData | null>(null);
  const [flip, setFlip] = useState<boolean>(false);
  const [revealButtons, setRevealButtons] = useState<boolean>(false);

  const ToggleFlip = () => {
    setFlip(!flip);
    if (revealButtons == false) setRevealButtons(true);
  };

  useEffect(() => {
    if (!!deckId) SetLoadStudy();
  }, [deckId]);

  const SetLoadStudy = async () => {
    let response = await LoadStudy(parseInt(deckId as string));
    if (response) {
      setData(response);
      setFlip(false);
    } else router.push("/studyDeckList");
  };

  const Answer = async (value: number) => {
    let response = await SaveStudy({
      answer: value,
      id: parseInt(deckId as string),
    });

    if (response) SetLoadStudy();
    else ToggleFlip();
  };

  if (!deckId || !data) return <></>;
  return (
    <>
      <Stack.Screen options={{ headerShown: false }} />
      <View style={styles.container}>
        <View style={styles.flashcardArea}>
          <FlashCard flip={flip} back={data.back} front={data.front} />
        </View>
        {flip ? (
          <View style={styles.buttonContent}>
            <View style={styles.buttonItem}>
              <Button
                type="danger"
                title="Errei"
                icon="close"
                onPress={() => Answer(1)}
              />
            </View>
            <View style={styles.buttonItem}>
              <Button
                type="warning"
                title="Dificil"
                icon="account-question"
                onPress={() => Answer(3)}
              />
            </View>
            <View style={styles.buttonItem}>
              <Button
                type="neutral"
                title="Moderado"
                icon="clock"
                onPress={() => Answer(4)}
              />
            </View>
            <View style={styles.buttonItem}>
              <Button
                type="success"
                title="Facil"
                icon="check"
                onPress={() => Answer(5)}
              />
            </View>
          </View>
        ) : (
          <View style={styles.buttonContent}>
            <View style={{ width: "80%" }}>
              <Button
                type="secondary"
                title="Revelar"
                icon="rotate-right"
                onPress={ToggleFlip}
              />
            </View>
          </View>
        )}
      </View>
    </>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors["light"].background,
    paddingTop: Constants.statusBarHeight,
  },
  flashcardArea: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
  },
  buttonContent: {
    flexDirection: "row",
    flexWrap: "wrap",
    alignItems: "center",
    justifyContent: "center",
    marginBottom: Spacing.m,
    gap: Spacing.s,
  },
  buttonItem: {
    width: "40%",
  },
});

export default StudyingDeck;
